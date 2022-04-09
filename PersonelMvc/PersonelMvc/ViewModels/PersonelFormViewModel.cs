using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonelMvc.Models.EntityFramework;

namespace PersonelMvc.ViewModels
{
    public class PersonelFormViewModel
    {
        public IEnumerable<Departman> Departmanlar { get; set; }
        public Personel Personel { get; set; }

    }
}