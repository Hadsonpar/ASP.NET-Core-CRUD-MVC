using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DEMO01_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DEMO01_CRUD.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }
        public HomeController(IConfiguration configuration) {
            Configuration = configuration;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<ClsUsuario> UsuariosList = new List<ClsUsuario>();

            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString)) {

                connection.Open();

                string sql = "select * from USUARIO";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        ClsUsuario clsUsuario = new ClsUsuario();
                        clsUsuario.id = Convert.ToInt32(dataReader["Id"]);
                        clsUsuario.usuario = Convert.ToString(dataReader["Usuario"]);
                        clsUsuario.contrasena = Convert.ToString(dataReader["Contrasena"]);
                        clsUsuario.intentos = Convert.ToInt32(dataReader["Intentos"]);
                        clsUsuario.nivelSeg = Convert.ToDecimal(dataReader["NivelSeg"]);
                        clsUsuario.fechaReg = Convert.ToDateTime(dataReader["FechaReg"]);

                        UsuariosList.Add(clsUsuario);
                    }
                }

                connection.Close();
            }
            return View(UsuariosList);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClsUsuario clsUsuario)
        {
            if (ModelState.IsValid) {
                string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    string sql = $"Insert Into USUARIO(Usuario, Contrasena, Intentos, NivelSeg) Values('{clsUsuario.usuario}', '{clsUsuario.contrasena}', '{clsUsuario.intentos}', '{clsUsuario.nivelSeg}')";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Index");
                }
            }
            else
            return View();
        }

        public IActionResult Edit(int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            ClsUsuario clsUsuario = new ClsUsuario();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From USUARIO Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        clsUsuario.id = Convert.ToInt32(dataReader["Id"]);
                        clsUsuario.usuario = Convert.ToString(dataReader["Usuario"]);
                        clsUsuario.contrasena = Convert.ToString(dataReader["Contrasena"]);
                        clsUsuario.intentos = Convert.ToInt32(dataReader["Intentos"]);
                        clsUsuario.nivelSeg = Convert.ToDecimal(dataReader["NivelSeg"]);
                        clsUsuario.fechaReg = Convert.ToDateTime(dataReader["FechaReg"]);

                    }
                }
                connection.Close();
            }
            return View(clsUsuario);
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult Edit_Post(ClsUsuario clsUsuario)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {             
                string sql = $"Update USUARIO SET Usuario='{clsUsuario.usuario}', Contrasena='{clsUsuario.contrasena}', Intentos='{clsUsuario.intentos}', NivelSeg='{clsUsuario.nivelSeg}' Where Id='{clsUsuario.id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            ClsUsuario clsUsuario = new ClsUsuario();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From USUARIO Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        clsUsuario.id = Convert.ToInt32(dataReader["Id"]);
                        clsUsuario.usuario = Convert.ToString(dataReader["Usuario"]);
                        clsUsuario.contrasena = Convert.ToString(dataReader["Contrasena"]);
                        clsUsuario.intentos = Convert.ToInt32(dataReader["Intentos"]);
                        clsUsuario.nivelSeg = Convert.ToDecimal(dataReader["NivelSeg"]);
                        clsUsuario.fechaReg = Convert.ToDateTime(dataReader["FechaReg"]);
                    }
                }
            }
            return View(clsUsuario);
        }

        [HttpGet]//Lo uso para ver el detalle del registro y para la eliminacion de registro
        public IActionResult Delete(int? id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            ClsUsuario clsUsuario = new ClsUsuario();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From USUARIO Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        clsUsuario.id = Convert.ToInt32(dataReader["Id"]);
                        clsUsuario.usuario = Convert.ToString(dataReader["Usuario"]);
                        clsUsuario.contrasena = Convert.ToString(dataReader["Contrasena"]);
                        clsUsuario.intentos = Convert.ToInt32(dataReader["Intentos"]);
                        clsUsuario.nivelSeg = Convert.ToDecimal(dataReader["NivelSeg"]);
                        clsUsuario.fechaReg = Convert.ToDateTime(dataReader["FechaReg"]);
                    }
                }
            }
            return View(clsUsuario);
        }

        //[HttpPost]//Se puede usar directo, invocando desde el view Index a través de un boton con HttpPost
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From USUARIO Where Id='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ViewBag.Result = "Error en la operación:" + ex.Message;
                    }
                    connection.Close();
                }
            }

            return RedirectToAction("Index");
        }
    }
}