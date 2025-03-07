﻿using System;
using GestorFinanceiro.Models;
using GestorFinanceiro.Repositorios;

class Program
{
    static void Main()
    {
        RepositorioTransacoes repositorio = new();

        while (true) 
        {
            Console.WriteLine("===== Gestor Financeiro =====");
            Console.WriteLine("1 - Adicionar Transação");
            Console.WriteLine("2 - Listar Transações");
            Console.WriteLine("3 - Consultar saldo disponível");
            Console.WriteLine("0- Sair");
            Console.Write("Escolha uma opção:");

            string option = Console.ReadLine();

            switch (option) 
            {
                case "1":
                    AddTransition(repositorio);
                    break;

                case "2":
                    ListTransitions(repositorio);
                    break;
                
                case "3":
                    decimal entradas, saidas;
                    decimal saldo = repositorio.CalcularSaldo(out entradas, out saidas); //Obter entradas e saidas separadas
                    Console.WriteLine($"Total de entradas: $ {entradas:F2}");
                    Console.WriteLine($"Total de saídas: $ {saldo:F2}");
                    Console.WriteLine($"Saldo disponível: $ {saldo:F2}");
                    Console.ReadKey();
                    break;

                case "0":
                    return;
                
                default:
                    Console.WriteLine("Opção inválida");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void AddTransition(RepositorioTransacoes repo)
    {
        Console.Write("Descrição: ");
        string descricao = Console.ReadLine();

        Console.Write("Valor: ");
        if(!decimal.TryParse(Console.ReadLine(), out decimal valor) || valor <= 0)
        {
            Console.WriteLine("Valor inválido!");
            Console.ReadKey();
            return;
        }

        Console.Write("Categoria: (ex: Casa, mercado) ");
        string categoria = Console.ReadLine();


        string tipo; //para não causar erro no repo.AddTransition
        while (true)
        {
            Console.Write("Tipo: (Entradas e saídas)"); 
             tipo = Console.ReadLine()?.Trim().ToLower(); //Remove espaços e converte o que é digitado para minusculas

            if(tipo == "entrada" || tipo == "saida") 
            {
                tipo = char.ToUpper(tipo[0]) + tipo.Substring(1); //serve para capitalizar a primeira letra (entrada -> Entrada)
                break;
            }
            Console.WriteLine("Tipo inválido! Escolha 'entrada' ou 'saida'");
        }
        repo.AddTransition(new Transacao
        {
            Descricao = descricao,
            Valor = valor,
            Categoria = categoria,
            Tipo = tipo
        });

        Console.Write("Transação adicionada com sucesso!");
        Console.ReadKey();
    }

    static void ListTransitions(RepositorioTransacoes repo)
    {
        var transacoes = repo.ListTransitions();

        if(transacoes.Count == 0)
        {
            Console.WriteLine("Nenhuma transação registrada.");
        }
        else
        {
            Console.Write("\n=== SUAS TRANSAÇÕES ===");
            Console.WriteLine("ID | Descrição | Tipo | Valor | Categoria | Data");
            Console.Write("--------------------------------------");

            foreach (var t in transacoes)
            { 
                Console.WriteLine($"{t.Id} | {t.Descricao} | {t.Tipo} | ${t.Valor} | {t.Categoria} | {t.Data:d}");
            }
        }
        Console.ReadKey();
    }
}