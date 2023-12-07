using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

public class ConexDB : MonoBehaviour
{
    // Declaraci�n de variables para la conexi�n a la base de datos
    string rutaDB;
    string strConexion;
    string DBFileName = "DBinventario.sqlite";
    IDbConnection dbConnection;
    IDbCommand dbCommand;
    IDataReader reader;
    // M�todo llamado al inicio del script
    void Start()
    {
        // Llama al m�todo para abrir la conexi�n a la base de datos
        AbrirDB();
    }
    // M�todo para abrir la base de datos
    void AbrirDB()
    {
        // Establecer la ruta de la base de datos seg�n la plataforma
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            rutaDB = Application.dataPath + "/StreamingAssets/" + DBFileName;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            rutaDB = Application.dataPath + "/Raw/" + DBFileName;
        }
        // Crear la cadena de conexi�n utilizando la biblioteca Mono.Data.Sqlite
        strConexion = "URI=file:" + rutaDB;
        // Inicializar y abrir la conexi�n a la base de datos SQLite
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
            // Mostrar la informaci�n en la consola de Unity
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

