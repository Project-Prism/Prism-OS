using static PrismProject.Filesystem.FSCore;

namespace PrismProject.Services
{
    class FTSE
    {
        public static void MakeFs()
        {
            Format(0);
            try { CreateFolder("0:\\Prism-Core\\"); } catch { }
            try { CreateFolder("0:\\Users\\"); } catch { }
            try { CreateFolder("0:\\Users\\Default\\"); } catch { }
            try { WriteFile("0:\\Prism-Core\\net.conf", "", false); } catch { }
            try { WriteFile("0:\\Prism-Core\\LastFormat.conf", Time.Month + "/" + Time.Day + "/" + Time.Year + " " + Time.Hour + ":" + Time.Minute + " " + Time.Seccond, false); } catch { }
        }
    }
}
