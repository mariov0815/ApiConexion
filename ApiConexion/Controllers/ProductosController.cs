using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using ApiConexion.Models;

namespace ApiConexion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly string connectionString = "Server=host23.latinoamericahosting.com;Database=funda1_pos88;Uid=funda1_userpos88;Pwd=1xFH{RqNP4Ze(3H^;Port=3306;";

        [HttpGet]
        public ActionResult<List<Producto>> Get()
        {
            var productos = new List<Producto>();

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM productos", conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    productos.Add(new Producto
                    {
                        Id = reader["id"].ToString(),
                        Descripcion = reader["descripcion"]?.ToString(),
                        Precio1 = reader["precio1"] as decimal?,
                        Precio2 = reader["precio2"] as decimal?,
                        Precio3 = reader["precio3"] as decimal?,
                        Nfrac = reader["nfrac"] as decimal?,
                        Existencia = reader["existencia"] as decimal?,
                        Grupo = reader["grupo"]?.ToString(),
                        Iva = reader["iva"] as decimal?,
                        Costo = reader["costo"] as decimal?,
                        Max = reader["max"]?.ToString(),
                        Min = reader["min"]?.ToString(),
                        Uni = reader["Uni"]?.ToString(),
                        Codigo2 = reader["codigo2"]?.ToString(),
                        Rf = reader["rf"]?.ToString()
                    });
                }
            }

            return productos;
        }

        [HttpPost]
        public ActionResult InsertarProducto([FromBody] Producto nuevo)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand(@"
                    INSERT INTO productos 
                    (id, descripcion, precio1, precio2, precio3, nfrac, existencia, grupo, iva, costo, max, min, Uni, codigo2, rf)
                    VALUES 
                    (@id, @descripcion, @precio1, @precio2, @precio3, @nfrac, @existencia, @grupo, @iva, @costo, @max, @min, @Uni, @codigo2, @rf)", conn);

                cmd.Parameters.AddWithValue("@id", nuevo.Id);
                cmd.Parameters.AddWithValue("@descripcion", nuevo.Descripcion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@precio1", nuevo.Precio1 ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@precio2", nuevo.Precio2 ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@precio3", nuevo.Precio3 ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@nfrac", nuevo.Nfrac ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@existencia", nuevo.Existencia ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@grupo", nuevo.Grupo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@iva", nuevo.Iva ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@costo", nuevo.Costo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@max", nuevo.Max ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@min", nuevo.Min ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Uni", nuevo.Uni ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@codigo2", nuevo.Codigo2 ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@rf", nuevo.Rf ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
            }

            return Ok(new { mensaje = "Producto insertado correctamente" });
        }
    }
}