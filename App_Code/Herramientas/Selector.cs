using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;

namespace Herramientas
{
    class Selector
    {
        public Selector()
        {
            Console.WriteLine("");
            Console.WriteLine("Se crea el objeto selector");
            Console.WriteLine("");

            this.Id = Id;
            this.Type = Type;
            this.Name = Name;

        }
        private string id;
        private string name;
        private string type;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public string Propiedades { get; set; }

    }
}
