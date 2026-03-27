using ContactManage.Models;
using Microsoft.AspNetCore.Mvc;


namespace ContactManage.Controllers
{
    public class ContactController : Controller
    {
        static List<ContactInfo> contacts = new List<ContactInfo>();

        public ActionResult ShowContacts()
        {
            return View(contacts);
        }

        public ActionResult GetContactById(int id)
        {
            var res = contacts.FirstOrDefault(x => x.ContactId == id);
            return View(res);
        }

        public ActionResult AddContact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddContact(ContactInfo contactInfo)
        {
            contacts.Add(contactInfo);
            return RedirectToAction("ShowContacts");
        }
    }
}