package Principal;

import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.io.RandomAccessFile;

public class main
{

	public static void main(String[] args)
	{
		try {
			exportarTexto("A01_00_000.bin", "A01_00_000_trans.txt");
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	public static void exportarTexto(String nombreBinario, String nombreExportado) throws IOException
	{
		RandomAccessFile lectorBinarios = new RandomAccessFile(nombreBinario, "r"); //Solo lectura de binarios
		FileWriter fw = new FileWriter(nombreExportado);
		PrintWriter salida = new PrintWriter(fw);
		
		//PASO 1: Leer de 4 en 4 bytes para encontrar dónde empieza el texto
		lectorBinarios.seek(20); //Salta/lee los primeros 5 grupos de 4 bytes
		
		//Leemos 4 bytes del 6to grupo
		int b1 = lectorBinarios.read();
		int b2 = lectorBinarios.read();
		int b3 = lectorBinarios.read();
		int b4 = lectorBinarios.read();
		
		
		salida.close();
		fw.close();
		lectorBinarios.close();
	}
}
