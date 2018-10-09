using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Security;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customers Customers { get; set; }

        public string Title
        {
            get
            {
                if (Customers != null && Customers.Id != 0)
                    return "Edit Customer";
                return "New Customer";
            }
        }
    }
}