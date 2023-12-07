using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

public class ConexDB : MonoBehaviour
{
    // Declaración de variables para la conexión a la base de datos
    string rutaDB;
    string strConexion;
    string DBFileName = "DBinventario.sqlite";
    IDbConnection dbConnection;
    IDbCommand dbCommand;
    IDataReader reader;
    // Método llamado al inicio del script
    void Start()
    {
        // Llama al método para abrir la conexión a la base de datos
        AbrirDB();
    }
    // Método para abrir la base de datos
    void AbrirDB()
    {
        // Establecer la ruta de la base de datos según la plataforma
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            rutaDB = Application.dataPath + "/StreamingAssets/" + DBFileName;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            rutaDB = Application.dataPath + "/Raw/" + DBFileName;
        }
        // Crear la cadena de conexión utilizando la biblioteca Mono.Data.Sqlite
        strConexion = "URI=file:" + rutaDB;
        // Inicializar y abrir la conexión a la base de datos SQLite
        dbConnection = new SqliteConnection(strConexion);
        dbConnection.Open();
        // Crear el comando de la base de datos y establecer la consulta SQL
        dbCommand = dbConnection.CreateCommand();
        string sqlQuery = "SELECT * FROM Armas";
        dbCommand.CommandText = sqlQuery;
        // Ejecutar la consulta y obtener un lector para leer los resultados
        reader = dbCommand.ExecuteReader();
        while (reader.Read())
        {
            // Obtener los valores de la fila
            int Id = reader.GetInt32(0);
            string Nombre = reader.GetString(1);
            int Danio = reader.GetInt32(2);
            // Mostrar la información en la consola de Unity
            Debug.Log(Id + " - " + Nombre + " - " + Danio);
        }
        // Cerrar las conexiones y liberar recursos
        reader.Close();
        reader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }
}

