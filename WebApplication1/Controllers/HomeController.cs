using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Entities;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    { 
        BaseDatos db = new BaseDatos();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string insertClient([FromBody] Client person)
        {
            string sql = "INSERT INTO client (Name, Last_Name, Document_ID) VALUES ('" + person.Name + "','" + person.Last_Name + "', '" + person.Document_ID + "');";
            string result = db.ejecutarSQL(sql);
            return result;
        }

        public IEnumerable<Client> selectClient(int ID)
        {
            string sql = "SELECT * FROM client WHERE (`ID` = " + ID + ")";
            DataTable dt = db.getTable(sql);

            List<Client> clientList = new List<Client>();
            clientList = (from DataRow dr in dt.Rows
                          select new Client()
                          {
                              ID = Convert.ToInt32(dr["ID"]),
                              Name = dr["Name"].ToString(),
                              Last_Name = dr["Last_Name"].ToString(),
                              Document_ID = dr["Document_ID"].ToString(),

                          }).ToList();
            return clientList;
        }

        public IEnumerable<Client> selectClients()
        {
            string sql = "SELECT * FROM client";
            DataTable dt = db.getTable(sql);

            List<Client> clientsList = new List<Client>();
            clientsList = (from DataRow dr in dt.Rows
                           select new Client()
                           {
                               ID = Convert.ToInt32(dr["ID"]),
                               Name = dr["Name"].ToString(),
                               Last_Name = dr["Last_Name"].ToString(),
                               Document_ID = dr["Document_ID"].ToString(),

                           }).ToList();
            return clientsList;
        }

        public string insertInvoice([FromBody] Invoice invoice)
        {
            string sql = "";
            sql += "INSERT INTO invoice (Id_Client, Cod) VALUES ("+ invoice.Id_Client + ", " + invoice.Cod + ");";
            sql += "SELECT @@identity AS ID;";
            foreach (Invoice_Detail item in invoice.details)
            {
                sql += "INSERT INTO invoice_detail (Id_Invoice,Description,Value) VALUES(@@identity,'" + item.Description + "', " + item.Value + ");";
            }
            string result = db.ejecutarSQL(sql);
            return result;
        }

        public IEnumerable<Invoice> selectInvoice(int ID)
        {
            string sql = "SELECT i.*, id.ID,id.description,id.value FROM invoice i INNER JOIN invoice_detail id ON id.Id_Invoice = i.ID WHERE id.Id_Invoice = " + ID + "";
            DataTable dt = db.getTable(sql);

            List<Invoice> invoiceList = new List<Invoice>();
            List<Invoice_Detail> invoiceDList = new List<Invoice_Detail>();

            invoiceDList = (from DataRow dr in dt.Rows
                           select new Invoice_Detail()
                           {
                               ID = Convert.ToInt32(dr["ID"]),
                               Description = dr["Description"].ToString(),
                               Value = Convert.ToInt32(dr["Value"])
                           }).ToList();

            invoiceList = (from DataRow dr in dt.Rows
                           select new Invoice()
                           {
                               ID = Convert.ToInt32(dr["ID"]),
                               Id_Client = Convert.ToInt32(dr["Id_Client"]),
                               Cod = Convert.ToInt32(dr["Cod"]),
                               details = invoiceDList,
                           }).ToList();
            return invoiceList;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
