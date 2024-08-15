using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class BankAccount
{
    public string AccountNumber { get; private set; }
    public string AccountHolder { get; private set; }
    private int Password { get; set; }
    private double Balance;
    private Random accountCounter;
    public BankAccount(string accountHolder,int pass, double initialBalance = 1000)
    {
        accountCounter = new Random();
        AccountNumber = GenerateAccountNumber();
        Password = pass;
        AccountHolder = accountHolder;
        Balance = initialBalance;
    }

    public string GenerateAccountNumber()
    {
        return accountCounter.Next(100000, 1000000).ToString();
    }

    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            Balance += amount;
            Console.WriteLine($"{amount} TL yatirildi. Güncel bakiye: {Balance} TL");
        }
        else
        {
            Console.WriteLine("Geçersiz tutar.");
        }
    }

    public void Withdraw(double amount)
    {
        if (amount > 0 && amount <= Balance)
        {
            Balance -= amount;
            Console.WriteLine($"{amount} TL çekildi. Güncel bakiye: {Balance} TL");
        }
        else
        {
            Console.WriteLine("Yetersiz bakiye veya geçersiz tutar.");
        }
    }

    public void DisplayBalance()
    {
        Console.WriteLine($"Hesap sahibi: {AccountHolder}, Hesap Numarasi: {AccountNumber}, Bakiye: {Balance} TL");
    }

    public string GetAccountHolder()
    {
        return AccountHolder;
    }

    public string GetAccountNumber()
    {
        return AccountNumber;
    }
}
class BankSystem
{
    private List<BankAccount> accounts = new List<BankAccount>();
    public void AddAccount(BankAccount account)
    {
        accounts.Add(account);
        Console.WriteLine($"Hesap başariyla oluşturuldu. Hesap Numaraniz: {account.GetAccountNumber()}");
    }

    public BankAccount FindAccount(string accountNumber)
    {
        return accounts.Find(account => account.GetAccountNumber() == accountNumber);
    }

    public void ListAccounts()
    {
        var i = 0;
        foreach (var item in accounts)
        {
            i++;
            System.Console.WriteLine($"{i}. Hesap => Ad : {item.GetAccountHolder()}");
        }
    }
}
class Program
{
    static void Main()
    {
        BankSystem bankySystem = new BankSystem(); 
        string choice;
        do
        {
            Console.WriteLine("\n1-Mevcut Hesaba Giriş Yap\n2-Yeni Hesap Oluştur\n3-Hesaplari Listele\nq-Çikiş");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Hesap numaranizi girin: ");
                    string accountNumber = Console.ReadLine();

                    BankAccount userAccount = bankySystem.FindAccount(accountNumber);

                    if (userAccount != null)
                    {
                        string input;
                        do
                        {
                            Console.WriteLine("\nİşlem Seçin: 1- Bakiye Görüntüleme, 2- Para Yatirma, 3- Para Çekme, q- Çikiş");
                            input = Console.ReadLine();

                            switch (input)
                            {
                                case "1":
                                    userAccount.DisplayBalance();
                                    break;
                                case "2":
                                    Console.Write("Yatirmak istediğiniz tutari girin: ");
                                    double depositAmount = Convert.ToDouble(Console.ReadLine());
                                    userAccount.Deposit(depositAmount);
                                    break;
                                case "3":
                                    Console.Write("Çekmek istediğiniz tutari girin: ");
                                    double withdrawAmount = Convert.ToDouble(Console.ReadLine());
                                    userAccount.Withdraw(withdrawAmount);
                                    break;
                            }

                        } while (input != "q");

                        Console.WriteLine("Çikiş yapildi.");
                    }
                    else
                    {
                        Console.WriteLine("Hesap bulunamadi.");
                    }
                    break;

                case "2":
                    string name;
                    int pass;
                    bool control;
                    do
                    {
                        Console.Write("Ad: ");
                        name = Console.ReadLine();
                        
                        Console.Write("Şifre: ");
                        string input = Console.ReadLine();
                        
                        control = int.TryParse(input, out pass);

                        if (string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("Ad boş olamaz. Lütfen tekrar deneyin.");
                            control = false;
                        }

                        if (!control)
                        {
                            Console.WriteLine("Geçersiz şifre girdisi. Lütfen sadece sayilar kullanin.");
                        }
                        
                    } while (!control);

                    BankAccount newAccount = new BankAccount(name,pass);
                    bankySystem.AddAccount(newAccount);
                    
                    break;
                case "3":
                    bankySystem.ListAccounts();
                    break;
                case "q":
                    Console.WriteLine("Programdan çikiliyor.");
                    break;
                default:
                    Console.WriteLine("Geçersiz seçenek, lütfen tekrar deneyin.");
                    break;
            }
        } while (choice != "q");
    }
}
