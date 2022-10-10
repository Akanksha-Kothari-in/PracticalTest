using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using PracticalTest1.Models;

namespace PracticalTest1.Controllers
{
    public class ProductController : Controller
    {
        string connectionString = @"Data Source = (local)\sqle2012; Initial Catalog = PracticalTest; Integrated Security=True";
        
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select OT.OrderID, OT.CustomerID, OT.ProductID,CT.FirstName,CT.LastName,CT.ContactNo,PT.Name from OrderTable as OT join CustomerTable as CT on CT.CustomerID = OT.OrderID join ProductTable as PT on PT.ProductID = OT.OrderID", sqlCon);
                sqlDa.Fill(dtblProduct);
            }
            return View(dtblProduct);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new OrderModal());
        }

        
        [HttpPost]
        public ActionResult Create(OrderModal orderModal)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "INSERT INTO OrderTable VALUES(@FirstName" + "@LastName,@Product,@Remarks)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@FirstName"+"@LastName", orderModal.Customer);
                sqlCmd.Parameters.AddWithValue("@Product", orderModal.Product);
                sqlCmd.Parameters.AddWithValue("@Remarks", orderModal.Remarks);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

      
        public ActionResult Edit(int id)
        {
            OrderModal orderModal = new OrderModal();
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select OT.OrderID, OT.CustomerID, OT.ProductID,CT.FirstName,CT.LastName,CT.ContactNo,PT.Name from OrderTable as OT join CustomerTable as CT on CT.CustomerID = OT.OrderID join ProductTable as PT on PT.ProductID = OT.OrderID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@OrderId", id);
                sqlDa.Fill(dtblProduct);
            }
            if (dtblProduct.Rows.Count == 1)
            {
                orderModal.OrderId = Convert.ToInt32(dtblProduct.Rows[0][0].ToString());
                orderModal.Customer = dtblProduct.Rows[0][1].ToString();
                orderModal.Product = dtblProduct.Rows[0][2].ToString();
                orderModal.Remarks = dtblProduct.Rows[0][3].ToString();
                return View(orderModal);
            }
            else
                return RedirectToAction("Index");
        }

      
        [HttpPost]
        public ActionResult Edit(OrderModal orderModal)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE OrderTable SET CustomerId = @CustomerId , OrderId= @OrderId , Remarks = @Remarks WHere OrderId = @OrderId";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@OrderId", orderModal.OrderId);
                sqlCmd.Parameters.AddWithValue("@CustomerId", orderModal.Customer);
                sqlCmd.Parameters.AddWithValue("@OrderId", orderModal.Product);
                sqlCmd.Parameters.AddWithValue("@Remarks", orderModal.Remarks);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE FROM OrderTable WHere OrderId = @OrderId";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@OrderId", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}
