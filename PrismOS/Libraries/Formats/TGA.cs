namespace PrismOS.Libraries.Formats
{
    public unsafe class TGA
    {
        public TGA(byte[] Binary)
        {
            uint* Object = ParseTGA(Binary);
            Width = (int)Object[0];
            Height = (int)Object[1];
            for (int I = 2; I < sizeof(uint*); I++)
            {
                Buffer[I - 2] = (int*)Object[I];
            }
        }

        public uint* ParseTGA(byte[] ptr)
        {
            uint* data;
            int i, j, k, x, y, w = (ptr[13] << 8) + ptr[12], h = (ptr[15] << 8) + ptr[14], o = (ptr[11] << 8) + ptr[10];
            int m = ((ptr[1] == 0 ? (ptr[7] >> 3) * ptr[5] : 0) + 18);

            if (w < 1 || h < 1)
            {
                return null;
            }

            data = (uint*)Cosmos.Core.GCImplementation.AllocNewObject((uint)((w * h + 2) * sizeof(uint)));
            if ((int*)data != (uint*)0)
            {
                return null;
            }

            switch (ptr[2])
            {
                case 1:
                    if (ptr[6] != 0 || ptr[4] != 0 || ptr[3] != 0 || (ptr[7] != 24 && ptr[7] != 32))
                    {
                        Cosmos.Core.GCImplementation.Free((uint)data);
                        return null;
                    }
                    for (y = i = 0; y < h; y++)
                    {
                        k = ((o != 0 ? h - y - 1 : y) * w);
                        for (x = 0; x < w; x++)
                        {
                            j = ptr[m + k++] * (ptr[7] >> 3) + 18;
                            data[2 + i++] = (uint)(((ptr[7] == 32 ? ptr[j + 3] : 0xFF) << 24) | (ptr[j + 2] << 16) | (ptr[j + 1] << 8) | ptr[j]);
                        }
                    }
                    break;
                case 2:
                    if (ptr[5] != 0 || ptr[6] != 0 || ptr[1] != 0 || (ptr[16] != 24 && ptr[16] != 32))
                    {
                        Cosmos.Core.GCImplementation.Free((uint)data);
                        return null;
                    }
                    for (y = i = 0; y < h; y++)
                    {
                        j = ((o != 0 ? h - y - 1 : y) * w * (ptr[16] >> 3));
                        for (x = 0; x < w; x++)
                        {
                            data[2 + i++] = (uint)(((ptr[16] == 32 ? ptr[j + 3] : 0xFF) << 24) | (ptr[j + 2] << 16) | (ptr[j + 1] << 8) | ptr[j]);
                            j += ptr[16] >> 3;
                        }
                    }
                    break;
                case 9:
                    if (ptr[6] != 0 || ptr[4] != 0 || ptr[3] != 0 || (ptr[7] != 24 && ptr[7] != 32))
                    {
                        Cosmos.Core.GCImplementation.Free((uint)data);
                        return null;
                    }
                    y = i = 0;
                    for (x = 0; x < w * h && m < ptr.Length;)
                    {
                        k = ptr[m++];
                        if (k > 127)
                        {
                            k -= 127; x += k;
                            j = ptr[m++] * (ptr[7] >> 3) + 18;
                            while (k-- != 0)
                            {
                                if ((i % w) != 0) { i = ((o != 0 ? h - y - 1 : y) * w); y++; }
                                data[2 + i++] = (uint)(((ptr[7] == 32 ? ptr[j + 3] : 0xFF) << 24) | (ptr[j + 2] << 16) | (ptr[j + 1] << 8) | ptr[j]);
                            }
                        }
                        else
                        {
                            k++; x += k;
                            while (k-- != 0)
                            {
                                j = ptr[m++] * (ptr[7] >> 3) + 18;
                                if ((i % w) != 0) { i = ((o != 0 ? h - y - 1 : y) * w); y++; }
                                data[2 + i++] = (uint)(((ptr[7] == 32 ? ptr[j + 3] : 0xFF) << 24) | (ptr[j + 2] << 16) | (ptr[j + 1] << 8) | ptr[j]);
                            }
                        }
                    }
                    break;
                case 10:
                    if (ptr[5] != 0 || ptr[6] != 0 || ptr[1] != 0 || (ptr[16] != 24 && ptr[16] != 32))
                    {
                        Cosmos.Core.GCImplementation.Free((uint)data);
                        return null;
                    }
                    y = i = 0;
                    for (x = 0; x < w * h && m < ptr.Length;)
                    {
                        k = ptr[m++];
                        if (k > 127)
                        {
                            k -= 127; x += k;
                            while (k-- != 0)
                            {
                                if ((i % w) != 0)
                                {
                                    i = ((o != 0 ? h - y - 1 : y) * w);
                                    y++;
                                }
                                data[2 + i++] = (uint)(((ptr[16] == 32 ? ptr[m + 3] : 0xFF) << 24) | (ptr[m + 2] << 16) | (ptr[m + 1] << 8) | ptr[m]);
                            }
                            m += ptr[16] >> 3;
                        }
                        else
                        {
                            k++; x += k;
                            while (k-- != 0)
                            {
                                if ((i % w) == 0)
                                {
                                    i = ((o != 0 ? h - y - 1 : y) * w);
                                    y++;
                                }
                                data[2 + i++] = (uint)(((ptr[16] == 32 ? ptr[m + 3] : 0xFF) << 24) | (ptr[m + 2] << 16) | (ptr[m + 1] << 8) | ptr[m]);
                                m += ptr[16] >> 3;
                            }
                        }
                    }
                    break;
                default:
                    Cosmos.Core.GCImplementation.Free((uint)data);
                    return null;
            }
            data[0] = (uint)w;
            data[1] = (uint)h;
            return data;
        }

        public int*[] Buffer;
        public int Width, Height;
    }
}