using System;
using System.Collections.Generic;
using System.Text;

namespace EzylBot.Services.Metier
{
    public class UrbanList
    {
        private List<Urban> list;

        public UrbanList(List<Urban> list)
        {
            this.List = list;
        }

        public List<Urban> List { get => list; set => list = value; }
    }

    public class Urban
    {
        private string definition;
        private string permalink;
        private string author;
        private string word;
        private string written_on;
        private string example;

        public Urban(string definition, string permalink, string author, string word, string written_on, string example)
        {
            this.Definition = definition;
            this.Permalink = permalink;
            this.Author = author;
            this.Word = word;
            this.Written_on = written_on;
            this.Example = example;
        }

        public string Definition { get => definition; set => definition = value; }
        public string Permalink { get => permalink; set => permalink = value; }
        public string Author { get => author; set => author = value; }
        public string Word { get => word; set => word = value; }
        public string Written_on { get => written_on; set => written_on = value; }
        public string Example { get => example; set => example = value; }
    }
}
