using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticalTask.Models;

namespace PracticalTask.Controllers
{
    public class HomeController : Controller
    {
        //Please provide your server info
        string connectionString = @"Data Source=(LocalDb)\v11.0; Integrated Security=true;Initial Catalog=PracticalTest";

        [HttpGet]
        public ActionResult Index()
       {
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from TblOrder", sqlCon);
                sqlDa.Fill(dtblProduct);
            }
            return View(dtblProduct);
        }

        public ActionResult Create()
        {
            return View(new OrderModel());
        }

        [HttpPost]
        public ActionResult Create(OrderModel orderModal)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "INSERT INTO TblOrder VALUES(@Customer,@Product,@Remarks)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Customer", orderModal.Customer);
                sqlCmd.Parameters.AddWithValue("@Product", orderModal.Product);
                sqlCmd.Parameters.AddWithValue("@Remarks", orderModal.Remarks);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            OrderModel orderModal = new OrderModel();
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select * from TblOrder";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@OrderId", id);
                sqlDa.Fill(dtblProduct);
            }
            if (dtblProduct.Rows.Count != 1)
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
        public ActionResult Edit(OrderModel orderModal)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE TblOrder SET Customer = @Customer , Product = @Product , Remarks = @Remarks WHere OrderId = @OrderId";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@OrderId", orderModal.OrderId);
                sqlCmd.Parameters.AddWithValue("@Customer", orderModal.Customer);
                sqlCmd.Parameters.AddWithValue("@Product", orderModal.Product);
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
                string query = "DELETE FROM TblOrder WHere OrderId = @OrderId";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@OrderId", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

    }
}