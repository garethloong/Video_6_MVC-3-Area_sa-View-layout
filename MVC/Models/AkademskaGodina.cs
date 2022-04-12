using MVC.Helper;

namespace MVC.Models
{
    public class AkademskaGodina:IEntity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Opis { get; set; }
       
    }
}
