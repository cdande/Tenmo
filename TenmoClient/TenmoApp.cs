using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TenmoClient.Models;
using TenmoClient.Services;
using TenmoServer.Models;

namespace TenmoClient
{
    public class TenmoApp
    {
        private readonly TenmoConsoleService console = new TenmoConsoleService();
        private readonly TenmoApiService tenmoApiService;
        private ApiUser loggedInUser;

        public TenmoApp(string apiUrl)
        {
            tenmoApiService = new TenmoApiService(apiUrl);
        }

        public void Run()
        {
            bool keepGoing = true;
            while (keepGoing)
            {
                // The menu changes depending on whether the user is logged in or not
                if (tenmoApiService.IsLoggedIn)
                {
                    keepGoing = RunAuthenticated();
                }
                else // User is not yet logged in
                {
                    keepGoing = RunUnauthenticated();
                }
            }
        }

        private bool RunUnauthenticated()
        {
            console.PrintLoginMenu();
            int menuSelection = console.PromptForInteger("Please choose an option", 0, 2, 1);
            while (true)
            {
                if (menuSelection == 0)
                {
                    return false;   // Exit the main menu loop
                }

                if (menuSelection == 1)
                {
                    // Log in
                    Login();
                    return true;    // Keep the main menu loop going
                }

                if (menuSelection == 2)
                {
                    // Register a new user
                    Register();
                    return true;    // Keep the main menu loop going
                }
                console.PrintError("Invalid selection. Please choose an option.");
                console.Pause();
            }
        }

        private bool RunAuthenticated()
        {
            console.PrintMainMenu(tenmoApiService.Username);
            int menuSelection = console.PromptForInteger("Please choose an option", 0, 6);
            if (menuSelection == 0)
            {
                // Exit the loop
                return false;
            }

            if (menuSelection == 1)
            {
                DisplayBalance();
            }

            if (menuSelection == 2)
            {
                DisplayTransfersbyUserId();
            }

            if (menuSelection == 3)
            {
                // View your pending requests
            }

            if (menuSelection == 4)
            {
                CreateSendTransfer();
                console.PrintSuccess("Transfer successfully created and sent.");
            }

            if (menuSelection == 5)
            {
                // Request TE bucks
            }

            if (menuSelection == 6)
            {
                // Log out
                tenmoApiService.Logout();
                loggedInUser = null;
                console.PrintSuccess("You are now logged out");
            }

            return true;    // Keep the main menu loop going
        }

        private void Login()
        {
            LoginUser loginUser = console.PromptForLogin();
            if (loginUser == null)
            {
                return;
            }

            try
            {
                ApiUser user = tenmoApiService.Login(loginUser);
                if (user == null)
                {
                    console.PrintError("Login failed.");
                }
                else
                {
                    loggedInUser = user;
                    console.PrintSuccess("You are now logged in");
                }
            }
            catch (Exception)
            {
                console.PrintError("Login failed.");
            }
            console.Pause();
        }

        private void Register()
        {
            LoginUser registerUser = console.PromptForLogin();
            if (registerUser == null)
            {
                return;
            }
            try
            {
                bool isRegistered = tenmoApiService.Register(registerUser);
                if (isRegistered)
                {
                    console.PrintSuccess("Registration was successful. Please log in.");
                }
                else
                {
                    console.PrintError("Registration was unsuccessful.");
                }
            }
            catch (Exception)
            {
                console.PrintError("Registration was unsuccessful.");
            }
            console.Pause();
        }
        private void DisplayBalance()
        {
            decimal balance = tenmoApiService.GetBalanceByUserId();
            console.DisplayBalance(balance);
        }

        private void DisplayTransfersbyUserId()
        {
            List<ApiTransfer> userTransfers = tenmoApiService.GetTransfersByUserId();
            Account currentUserAccount = tenmoApiService.GetAccountByUserId(tenmoApiService.UserId);
            User transferUser = null;
            bool isFrom = false;
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Transfers");
            Console.WriteLine("ID          From/To                 Amount");
            Console.WriteLine("-------------------------------------------");
            
            foreach (ApiTransfer transfer in userTransfers)
            {
                
                if (transfer.AccountFrom == currentUserAccount.AccountId)
                {
                    transferUser = GetUserByAccountId(transfer.AccountTo);
                    isFrom = true;
                }
                else if (transfer.AccountTo == currentUserAccount.AccountId)
                {
                    transferUser = GetUserByAccountId(transfer.AccountFrom);
                    isFrom = false;
                }
                console.DisplayTransfersByUserId(transfer, transferUser, isFrom);
            }
            console.Pause("Press enter to continue: ");
        }

        private User GetUserByAccountId(int id)
        {
            User user = tenmoApiService.GetUserByAccountId(id);
            return user;
        }
        private ApiTransfer CreateSendTransfer()
        {
            console.DisplayAllUsers(tenmoApiService.GetAllUsers());
            ApiTransfer transfer = new ApiTransfer();
            int accountToSendTo = console.PromptForInteger("Id of the user your are sending to");
            transfer.AccountTo = tenmoApiService.GetAccountByUserId(accountToSendTo).AccountId;
            decimal amountToSend = console.PromptForDecimal("Enter amount to send");
            transfer.Amount = amountToSend;
            transfer.AccountFrom = tenmoApiService.GetAccountByCurrentUserId().AccountId;
            transfer.TransferStatusId = 2;
            transfer.TransferTypeId = 2;
            return tenmoApiService.CreateSendTransfer(transfer);
            
        }

    }
}
