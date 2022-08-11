using System.Collections.Generic;
using System;

namespace PrismOS.Libraries.Resource.Text.INI
{
    internal class INIFile<T>
    {
        public INIFile(string INI)
        {
            Objects = new();
        }

        internal List<(string Section, string Name, T Value)> Objects;

        public void SetValue(string Section, string Name, T Value)
        {
            for (int I = 0; I < Objects.Count; I++)
            {
                if (Objects[I].Name == Name && Objects[I].Section == Section)
                {
                    Objects[I] = (Section, Name, Value);
                    return;
                }
            }
            throw new Exception("Entry Not Found!");
        }
        public T GetValue(string Section, string Name)
        {
            for (int I = 0; I < Objects.Count; I++)
            {
                if (Objects[I].Name == Name && Objects[I].Section == Section)
                {
                    return Objects[I].Value;
                }
            }
            throw new Exception("Entry Not Found!");
        }
    }
}
