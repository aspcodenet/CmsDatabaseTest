using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DatabaseTest
{
    class SupplierAdmin
    {
        public void Run()
        {
            //Visa i datagridview
            var rep = new NorthwindRepository();
            var l = rep.GetAllSuppliers();

            //CLick på en -
            int id = 26;
            EditForm(id);
        }


        private void NewForm(int id)
        {
            var supplier = new Supplier();

            //2. Ta alla suppliers egenskaper och fyll formens textboxes utifrpn det
            //txtNamn.Text = supplier.CompanyName;
            //txtContact.Text = supplier.ContactName;

            ///3. On save CLICK - flytta alla textboxes till supplier
            /// supplier.CompanyName = txtNamn.Text;
            /// supplier.ContactName = txtContact.Text;
            /// osv osv
            /// Nu har vi ett supplierobjekt som är ändrat

            var rep = new NorthwindRepository();
            rep.Insert(supplier);
        }


        private void EditForm(int id)
        {
            var rep = new NorthwindRepository();
            var supplier = rep.GetSupplier(id);

            //2. Ta alla suppliers egenskaper och fyll formens textboxes utifrpn det
            //txtNamn.Text = supplier.CompanyName;
            //txtContact.Text = supplier.ContactName;

            ///3. On save CLICK - flytta alla textboxes till supplier
            /// supplier.CompanyName = txtNamn.Text;
            /// supplier.ContactName = txtContact.Text;
            /// osv osv
            /// Nu har vi ett supplierobjekt som är ändrat

            rep.Update(supplier);
        }
    }
}
