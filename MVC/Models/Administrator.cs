using MVC.Helper;

namespace MVC.Models
{
    public class Administrator : IEntity
    {
		public int Id { get; set; }


		public bool IsDeleted { get; set; }

		public Korisnik Korisnik { get; set; }
	}
}
