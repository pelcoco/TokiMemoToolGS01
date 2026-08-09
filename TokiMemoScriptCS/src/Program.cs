using System;
using System.IO;

public class Program
{
	public static void Main(string[] args)
	{
        int msgPointer;
		msgPointer = ExportarTexto("A01_00_000", "A01_00_000_trans");
        ImportarTexto("A01_00_000_trans", "A01_00_000", "A01_00_000_mod", msgPointer);
	}

	public static int ExportarTexto(string nombreFichero, string nombreExportado)
	{
        int msgPointer = 0; //Dirección de comienzo del texto
        int id = 0;
        string mensaje = "";
        int pos;

		using FileStream fs = new(nombreFichero, FileMode.Open, FileAccess.Read); //Puntero de solo lectura
        using BinaryReader lectorBinarios = new(fs); //Conversor de bytes a decimal
		using StreamWriter salida = new(nombreExportado);

		// Leer de 4 en 4 bytes y saltamos 5 posiciones al byte 20 porque es donde empieza el diálogo
		fs.Seek(20, SeekOrigin.Begin);

		//Posicionada en el byte 20, hay que leer la dirección de los 4 bytes (00 00 00 00)
		msgPointer += lectorBinarios.ReadInt32();

        fs.Seek(msgPointer, SeekOrigin.Begin); //Hemos encontrado el comienzo del diálogo
        pos = lectorBinarios.ReadByte(); //Leemos byte a byte para traducir carácter a carácter

        while (pos != -1)
        {
            mensaje += (char)pos;
            pos = lectorBinarios.ReadByte();
            if (pos == 0x00)
            {
                salida.WriteLine("//---------------------------------");
                salida.WriteLine($"[ID: "+id+"]");
                salida.WriteLine(mensaje);
                id++;
                mensaje = "";
                pos = lectorBinarios.ReadByte();
            }
        }
        return msgPointer;
    }

    public static void ImportarTexto(string txtTraducido, string rutaBinOriginal, string rutaBinMod, int msgPointer)
    {
        File.Copy(rutaBinOriginal, rutaBinMod, true); //Hago una copia para no tocar el original. (el true es para permitir sobreescribir LA COPIA)
        using FileStream fsTxt = new (txtTraducido, FileMode.Open, FileAccess.Read);
        using StreamReader lector = new(fsTxt);

        using FileStream fsBin = new (rutaBinMod, FileMode.Open, FileAccess.Write);
        using BinaryWriter conversor = new (fsBin);

        

    }
}
