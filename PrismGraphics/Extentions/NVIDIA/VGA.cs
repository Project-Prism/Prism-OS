#if IncludeNVIDIA

// # include <linux/vgaarb.h>
// # include <linux/vga_switcheroo.h>
// # include <drm/drm_fb_helper.h>
// # include "nouveau_drv.h"
// # include "nouveau_acpi.h"
// # include "nouveau_vga.h"

using Cosmos.HAL;

namespace PrismGraphics.Extentions.NVIDIA
{
	public unsafe class VGA
	{
		public static uint VGASetDecode(PCIDevice pdev, bool State)
		{
			nouveau_drm drm = new(pci_get_drvdata(pdev));
			nvif_object device = drm.client.device;

			if (drm.client.device.info.family == NV_DEVICE_INFO_V0_CURIE && drm.client.device.info.chipset >= 0x4c)
				nvif_wr32(device, 0x088060, State);
			else
			if (drm.client.device.info.chipset >= 0x40)
				nvif_wr32(device, 0x088054, State);
			else
				nvif_wr32(device, 0x001854, State);

			if (State)
				return VGA_RSRC_LEGACY_IO | VGA_RSRC_LEGACY_MEM |
					   VGA_RSRC_NORMAL_IO | VGA_RSRC_NORMAL_MEM;
			else
				return VGA_RSRC_NORMAL_IO | VGA_RSRC_NORMAL_MEM;
		}

		static void SwitcherooSetState(PCIDevice pdev, VGASwitcherooState State)
		{
			drm_device dev = pci_get_drvdata(pdev);

			if ((nouveau_is_optimus() || nouveau_is_v1_dsm()) && State == VGA_SWITCHEROO_OFF)
				return;

			if (State == VGA_SWITCHEROO_ON)
			{
				Console.WriteLine("VGA switcheroo: switched nouveau on");
				dev.switch_power_state = DRM_SWITCH_POWER_CHANGING;
				nouveau_pmops_resume(pdev.dev);
				dev.switch_power_state = DRM_SWITCH_POWER_ON;
			}
			else
			{
				Console.WriteLine("VGA switcheroo: switched nouveau off");
				dev.switch_power_state = DRM_SWITCH_POWER_CHANGING;
				nouveau_switcheroo_optimus_dsm();
				nouveau_pmops_suspend(pdev.dev);
				dev.switch_power_state = DRM_SWITCH_POWER_OFF;
			}
		}

		static void SwitcherooReprobe(PCIDevice pdev)
		{
			drm_device dev = pci_get_drvdata(pdev);
			drm_fb_helper_output_poll_changed(dev);
		}

		static bool SwitcherooCanSwitch(PCIDevice pdev)
		{
			drm_device* dev = pci_get_drvdata(pdev);

			/*
			 * FIXME: open_count is protected by drm_global_mutex but that would lead to
			 * locking inversion with the driver load path. And the access here is
			 * completely racy anyway. So don't bother with locking for now.
			 */
			return atomic_read(dev.open_count) == 0;
		}

		//		static const struct vga_switcheroo_client_ops nouveau_switcheroo_ops = {
		//	.set_gpu_state = nouveau_switcheroo_set_state,
		//	.reprobe = nouveau_switcheroo_reprobe,
		//	.can_switch = nouveau_switcheroo_can_switch,
		//};

		void VGAInit(nouveau_drm drm)
		{
			drm_device dev = drm.dev;
			bool runtime = nouveau_pmops_runtime();
			PCIDevice pdev;

			/* only relevant for PCI devices */
			if (!dev_is_pci(dev.dev))
				return;
			pdev = to_pci_dev(dev.dev);

			vga_client_register(pdev, nouveau_vga_set_decode);

			/* don't register Thunderbolt eGPU with vga_switcheroo */
			if (pci_is_thunderbolt_attached(pdev))
				return;

			vga_switcheroo_register_client(pdev, &nouveau_switcheroo_ops, runtime);

			if (runtime && nouveau_is_v1_dsm() && !nouveau_is_optimus())
				vga_switcheroo_init_domain_pm_ops(drm.dev.dev, ref drm.vga_pm_domain);
		}

		void VGAFini(nouveau_drm drm)
		{
			drm_device dev = drm.dev;
			bool runtime = nouveau_pmops_runtime();
			PCIDevice pdev;

			/* only relevant for PCI devices */
			if (!dev_is_pci(dev.dev))
				return;
			pdev = to_pci_dev(dev.dev);

			vga_client_unregister(pdev);

			if (pci_is_thunderbolt_attached(pdev))
				return;

			vga_switcheroo_unregister_client(pdev);
			if (runtime && nouveau_is_v1_dsm() && !nouveau_is_optimus())
				vga_switcheroo_fini_domain_pm_ops(drm.dev.dev);
		}

		void VGALastClose(drm_device dev)
		{
			vga_switcheroo_process_delayed_switch();
		}
	}
}

#endif