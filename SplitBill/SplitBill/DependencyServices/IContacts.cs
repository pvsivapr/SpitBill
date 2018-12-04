using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SplitBill.DependencyServices
{
    public interface IContacts
    {
        Task<List<Contact>> GetAllContacts();
    }

    public class Contact
    {
        public string ContactId { get; set; }
        public string ContactName { get; set; }
        public string DisplayName { get; set; }
        public string PrimaryContactNumber { get; set; }
        public List<double> AllContactNumbers { get; set; }
        public string xyz { get; set; }
    }
}
