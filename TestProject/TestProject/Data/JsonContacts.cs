using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Data
{
    public class Contactone
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool isservicecharge { get; set; }
        public decimal CostPrice { get; set; }
        public string Date { get; set; }
        //public string Image { get; set; }
        // public string ImageBase64 { private get; set; }
        public byte[] Image { get; set; }
        //{
        //    get
        //    {
        //        if (ImageBase64 != "" && ImageBase64 != null)
        //        {
        //            byte[] image = Convert.FromBase64String(ImageBase64);
        //            return image;
        //        }

        //        return null;
        //    }
        //}
    }

    public class ContectList
    {
        public List<Contactone> contacts { get; set; }
    }
}
