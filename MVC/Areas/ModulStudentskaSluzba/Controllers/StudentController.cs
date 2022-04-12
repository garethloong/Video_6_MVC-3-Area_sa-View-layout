using MVC.DAL;
using MVC.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Areas.ModulStudentskaSluzba.Controllers
{
    public class StudentController : Controller
    {
        // Primjer 1 (bez koristenja Viewa) - pristupamo sa: http://localhost:50913/Student/Racunaj?a=3&b=5
        //public string Racunaj(int a, int b)
        //{
        //    return "suma je: " + (a + b);
        //}

        /////////////////////////////////////////////////////////////////////////////////////////////

        // primjer 2 (Koristenjem Viewa) - pristupamo sa: http://localhost:50913/Student/PrikaziDatum)
        //public ActionResult PrikaziDatum()
        //{
        //    DateTime x = DateTime.Now;
        //    ViewData["x"] = x;      // prebacivanje podataka iz controllera na view: ViewData ili ViewBag

        //    return View("Prikazi");     // PrikaziDatum() koristi isti View kao i Prikazi()
        //}


        MyContext mc = new MyContext();

        public ActionResult Index() // defaultna akcija (kada unesemo samo http://localhost:50913/Student/)
        {
            return View("Index");
        }


        // primjer 3 (Koristenjem Viewa) - pristupamo sa: http://localhost:50913/Student/Prikazi)
        public ActionResult Prikazi()
        {
            List<Student> studenti = mc.Studenti
                .Include(x => x.Korisnik)     // moramu ukljuciti entitet "Korisnik" zato sto na View koristimo atribut "Ime_prezime" ovog entiteta
                .Include(x => x.Smjer)
                .Include(x => x.Smjer.Fakultet)
                .ToList();
            ViewData["studenti"] = studenti;

            return View("Prikazi");
        }

        public ActionResult Obrisi(int studentId)
        {
            Student s = mc.Studenti.Where(x => x.Id == studentId).Include(x => x.Korisnik).FirstOrDefault();
            mc.Studenti.Remove(s);
            mc.SaveChanges();

            return RedirectToAction("Prikazi");
        }

        //  http://localhost:50913/Student/Dodaj
        public ActionResult Dodaj()
        {
            Student s = new Student();
            s.Korisnik = new Korisnik();
            ViewData["student"] = s;

            List<Smjer> smjerovi = mc.Smjerovi
                .Include(x => x.Fakultet)
                .ToList();
            ViewData["smjerovi"] = smjerovi;

            return View("Uredi");
        }

        public ActionResult Uredi(int studentId)
        {
            // Student student = mc.Studenti.Find(studentId);   // Korisnik objekat se ne nalazi u objektu student
            Student student = mc.Studenti.Where(x => x.Id == studentId).Include(x => x.Korisnik).FirstOrDefault();      // Include() ne radi sa Find() pa moramo koristiti Where()

            ViewData["student"] = student;

            List<Smjer> smjerovi = mc.Smjerovi
              .Include(x => x.Fakultet)
              .ToList();
            ViewData["smjerovi"] = smjerovi;

            return View("Uredi");
        }

        public ActionResult Snimi(int? studentId, string ime, string prezime, string brojindexa, string username, string password, DateTime datumRodjenja, int smjerId)
        {
            Student s;

            if (studentId == null || studentId == 0)
            {
                s = new Student();
                s.Korisnik = new Korisnik();
                mc.Studenti.Add(s);     // Mozemo prvo dodati objekat pa ga onda setovat i obrnuto - redoslijed nije bitan (objekat se nalazi u memoriji)
            }
            else
            {
                s = mc.Studenti.Where(x => x.Id == studentId).Include(x => x.Korisnik).FirstOrDefault();
            }

            // setovanje objekta
            s.Korisnik.Ime = ime;
            s.Korisnik.Prezime = prezime;
            s.Korisnik.Username = username;
            s.Korisnik.Password = password;
            s.Korisnik.DatumRodjenja = datumRodjenja;
            s.BrojIndexa = brojindexa;
            s.SmjerId = smjerId;

            mc.SaveChanges();   // snima objekat u bazu

            return RedirectToAction("Prikazi");
        }





    }
}