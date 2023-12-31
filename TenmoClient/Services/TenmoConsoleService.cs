﻿using System;
using System.Collections.Generic;
using TenmoClient.Models;
using TenmoServer.Models;

namespace TenmoClient.Services
{
    public class TenmoConsoleService : ConsoleService
    {
        /************************************************************
            Print methods
        ************************************************************/
        public void PrintLoginMenu()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("Welcome to TEnmo!");
            Console.WriteLine("1: Login");
            Console.WriteLine("2: Register");
            Console.WriteLine("0: Exit");
            Console.WriteLine("---------");
        }

        public void PrintMainMenu(string username)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"Hello, {username}!");
            Console.WriteLine("1: View your current balance");
            Console.WriteLine("2: View your past transfers");
            Console.WriteLine("3: View your pending requests");
            Console.WriteLine("4: Send TE bucks");
            Console.WriteLine("5: Request TE bucks");
            Console.WriteLine("6: Log out");
            Console.WriteLine("0: Exit");
            Console.WriteLine("---------");
        }
        public LoginUser PromptForLogin()
        {
            string username = PromptForString("User name");
            if (String.IsNullOrWhiteSpace(username))
            {
                return null;
            }
            string password = PromptForHiddenString("Password");

            LoginUser loginUser = new LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }



        // Add application-specific UI methods here...
        public void DisplayBalance(decimal balance)
        {
            Console.WriteLine($"Your balance is: {balance}");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }


        public void DisplayTransfersByUserId(ApiTransfer transfer, User user, bool isFrom)
        {
            if (isFrom == true)
            {
                Console.WriteLine($"{transfer.TransferId}          To: {user.Username}         $ {transfer.Amount}");
            }
            else if (isFrom == false)
            {
                Console.WriteLine($"{transfer.TransferId}          From: {user.Username}         $ {transfer.Amount}");
            }
            Console.WriteLine("\n");

        }
        public void DisplayAllUsers(List<ApiUser> users)
        {
            Console.WriteLine("|-------------- Users --------------|");
            Console.WriteLine("|    Id | Username                  |");
            Console.WriteLine("|-------+---------------------------|");
            foreach(ApiUser element in users)
            {
                Console.WriteLine($"|  {element.UserId} | {element.Username}                   |");
            }
            Console.WriteLine("|-----------------------------------|");
           
        }

    }
}
