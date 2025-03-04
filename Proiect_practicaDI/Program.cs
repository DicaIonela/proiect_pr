﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NivelStocareDate;
using System.Configuration;
using LibrarieClase;
using System.Windows.Forms;
using InterfataUtilizator;
using System.Runtime.InteropServices;
/*using static System.Net.Mime.MediaTypeNames;*/
namespace Proiect_practicaDI
{
    using WinFormsApp = System.Windows.Forms.Application;

    internal class Program
        {

        [STAThread]
            static void Main(string[] args)
            {
            // Setăm aplicația pentru a folosi Windows Forms
            Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Afișăm fereastra de alegere
                DialogResult result = MessageBox.Show(
                    "Alegeți modul de utilizare:\n\n" +
                    "Yes - Interfață grafică\n" +
                    "No - Command Prompt",
                    "Selectați modul de utilizare",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Deschide interfața grafică
                    Application.Run(new InterfataGrafica());
                }
                else
                {
                    // Continuă cu modul Command Prompt
                    StartCommandPromptMode();
                }
            }
        
        static void StartCommandPromptMode()
        {
            Init.Initialize(out Administrare_FisierText admin, out Utilizator utilizatornou);
            Metode.Meniu();
            do
            {
                Console.WriteLine("\nIntrodu optiunea dorita:");
                string optiune = Console.ReadLine();
                switch (optiune)
                {
                    case "C":
                        utilizatornou = Metode.CitireUtilizatorTastatura();
                        break;
                    case "S":
                        /* VERIFICARE NUMAR DE TELEFON VALID */ 
                        if (utilizatornou!=null)
                        { 
                            admin.AddUtilizator(utilizatornou);
                            Console.WriteLine("Utilizatorul a fost adaugat cu succes.");
                        }
                        else
                        {
                            Console.WriteLine("Utilizatorul este deja in fisier.");
                        }
                        break;
                    case "A":
                        Utilizator[] utilizatori = admin.GetUtilizatori(out int nrUtilizatori);/*SE CREEAZA UN TABLOU DE OBIECTE*/
                        Metode.AfisareUtilizatori(utilizatori, nrUtilizatori);
                        break;
                    case "L":
                        Console.WriteLine("Introduceti criteriul de cautare:");
                        string criteriu = Console.ReadLine();
                        Utilizator[] utilizatoriGasiti = admin.CautaUtilizator(criteriu);
                        if (utilizatoriGasiti.Length > 0)
                        {
                            Metode.AfisareUtilizatori(utilizatoriGasiti, utilizatoriGasiti.Length);
                        }
                        else
                        {
                            Console.WriteLine("Nu s-au găsit utilizatori care să corespundă criteriului.");
                        }
                        break;
                    case "M":
                        string adresam = Metode.GetMacAddress();
                        Console.WriteLine("Adresa MAC a calculatorului este: " + adresam);
                        break;
                    case "E":
                        Console.WriteLine("Introdu numele utilizatorului de sters:");
                        string numedesters=Console.ReadLine();
                        admin.StergeUtilizator(numedesters);
                        break;
                }

            } while (true);
            
        }
    }
}
