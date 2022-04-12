using MVC.Helper;

namespace MVC.Models
{
    public class Fakultet:IEntity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Naziv { get; set; }


    }
}
