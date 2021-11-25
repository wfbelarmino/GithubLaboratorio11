using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoLinq
{
   internal class Program
   {
      static void Main(string[] args)
      {
         List<Pessoa> pessoas = new List<Pessoa>
         {
            new Pessoa{Nome="Ana", DataNascimento=new DateTime(1980,3,14), Casada=true},
            new Pessoa{Nome="Paulo", DataNascimento=new DateTime(1978,10,23), Casada=false},
            new Pessoa{Nome="Ana Maria", DataNascimento=new DateTime(1980,3,14), Casada=false},
            new Pessoa{Nome="Carlos", DataNascimento=new DateTime(1999,12,12), Casada=true},
            new Pessoa{Nome="Pedro", DataNascimento=new DateTime(1970,5,27), Casada=false},
            new Pessoa{Nome="Huginho", DataNascimento=new DateTime(1970,5,27), Casada=true},
            new Pessoa{Nome="Zezinho", DataNascimento=new DateTime(1980,3,17), Casada=false},
            new Pessoa{Nome="Luizinho", DataNascimento=new DateTime(1990,7,7), Casada=false}
         };

         //Consulta tradiconal
         List<Pessoa> resultado1 = new List<Pessoa>();
         foreach (Pessoa p in pessoas)
         {
            if (p.Casada)
               resultado1.Add(p);
         }
         Console.WriteLine("Pessoas casadas, consulta tradiconal:");

         foreach (Pessoa p in resultado1)
         {
            Console.WriteLine(p.Nome);
         }

         //Consulta LINQ
         var resultado2 = from p in pessoas
                          where p.Casada
                          select p;
         Console.WriteLine("\nPessoas casadas, consulta linq:");

         foreach (Pessoa p in resultado2)
         {
            Console.WriteLine(p.Nome);
         }

         Console.WriteLine("\nPessoas casadas, consulta linq (method syntax):");
         var resultado3 = pessoas.Where(p => p.Casada);

         foreach (Pessoa p in resultado3)
         {
            Console.WriteLine(p.Nome);
         }

         // para obter uma "lista" a partir da consulta linq
         var resultado4 = (from p in pessoas
                           where p.Casada
                           select p).ToList();


         Console.WriteLine("\nPessoas casadas que nasceram após 01/01/1980");
         var resultado5 = (from p in pessoas
                           where p.Casada && p.DataNascimento >= new DateTime(1980, 1, 1)
                           select p).ToList();

         // ups! forçando um pouco a barra...
         resultado5.ForEach(p => Console.WriteLine(p));

         Console.WriteLine("\nProjeção sobre o nome das pessoas casadas");
         var resultado6 = from p in pessoas
                          where p.Casada
                          select p.Nome;

         foreach (String s in resultado6)
         {
            Console.WriteLine(s);
         }

         Console.WriteLine("\nSoteiros em uma lista de objetos anonimos..");
         var resultado7 = (from p in pessoas
                           where !p.Casada
                           select new { p.Nome, p.DataNascimento }).ToList();

         foreach (var elem in resultado7)
         {
            Console.WriteLine(elem.Nome + " " + elem.DataNascimento);
         }

         Console.WriteLine("\nPessoas agrupadas pelo estado civil");
         var resultado8 = from p in pessoas
                          group p by p.Casada;
         foreach (var g in resultado8)
         {
            Console.WriteLine($"casado:{g.Key}");
            foreach (var p in g)
            {
               Console.WriteLine(p);
            }
         }

         Console.WriteLine("\nPessoa mais nova");
         var resultado9 = pessoas.Min(p => p.DataNascimento);
         //Console.WriteLine("Consulta 9:");
         //Console.WriteLine(resultado9);

         var resultado10 = (from p in pessoas
                            where p.DataNascimento == resultado9
                            orderby p.DataNascimento, p.Nome
                            select p.Nome).ToList();

         resultado10.ForEach(x => Console.WriteLine(x));

         Console.ReadLine();


            Console.WriteLine("\nPessoa mais velha");
            var resultado11 = pessoas.Min(p => p.DataNascimento);
            //Console.WriteLine("Consulta 9:");
            //Console.WriteLine(resultado9);

            var resultado12 = (from p in pessoas
                               where p.DataNascimento >= resultado11
                               orderby p.DataNascimento
                               select p.Nome).ToList();

            resultado12.ForEach(x => Console.WriteLine(x));
            Console.WriteLine("Consulta 9:" + resultado11);
            Console.ReadLine();


            Console.WriteLine("\nSoteiros mais novos..");
            var resultado13 = (from p in pessoas
                              where !p.Casada
                              orderby p.DataNascimento
                              select new { p.Nome, p.DataNascimento }).ToList();

            foreach (var elem in resultado13)
            {
                Console.WriteLine(elem.Nome + " " + elem.DataNascimento);
            }


            Console.WriteLine("\nMedia de Idade");
            var resultado14 = (from p in pessoas
                              where p.Casada && p.DataNascimento >= new DateTime(1980, 1, 1)
                              orderby p.DataNascimento
                               select p).ToList();

             resultado14.ForEach(p => Console.WriteLine(p));


        }

   }

}
