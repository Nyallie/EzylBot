using System;
using System.Collections.Generic;
using System.Text;

namespace EzylBot.Services.Metier
{
    public class DataJsonNeko
    {
        private string url;

        public string Url { get => url; set => url = value; }

        public DataJsonNeko(string lin)
        {
            Url = lin;
        }
    }
}
