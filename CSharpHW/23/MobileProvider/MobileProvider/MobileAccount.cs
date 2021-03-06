﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace MobileProvider
{
    public class MobileAccount : IMobileAccount, IValidatableObject
    {
        public event EventHandler<ConnectionEventArgs> SendSmsProcessing;
        public event EventHandler<ConnectionEventArgs> MakeCallProcessing;
        
        private List<MobileAddress> _addresses = new List<MobileAddress>();
        
        public int Number { get; set; }
        
        public string Name { get; protected set; }
        
        public string Surname { get; protected set; }
        
        public string Email { get; protected set; }
        
        public int BirthYear { get; protected set; }
        
        public List<MobileAddress> Addresses
        {
            get { return _addresses; }
        }

        public MobileAccount(string name, string surname, string email, int birthYear)
        {
            Name = name;
            Surname = surname;
            Email = email;
            BirthYear = birthYear;
        }

        public void AddAddress(int number, string name)
        {
            if (!_addresses.Any(p => p.Number == number && p.Name == name))
            {
                _addresses.Add(new MobileAddress()
                {
                    Name = name,
                    Number = number
                });
            }
            else
            {
                var foundedAddress = _addresses.FirstOrDefault(p => p.Number == number);

                if (foundedAddress != null)
                {
                    foundedAddress.Name = name;
                }
            }
        }

        public void AddAddress(Dictionary<int, string> items)
        {
            foreach (KeyValuePair<int, string> item in items)
            {
                AddAddress(item.Key, item.Value);
            }
        }

        public void SendSms(string message, int receiver)
        {
            SendSmsProcessing?.Invoke(this, new ConnectionEventArgs(message, receiver));
        }

        public void ReceiveSms(string message, int sender)
        {
            if (IsConnectionValid(sender))
            {
                string senderName = GetSenderNameFromAddressBook(sender);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Account {Number}: Message from {sender}({senderName}) - \"{message}\"");
                Console.ResetColor();

                return;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Account {Number}: Message from non-whitelisted account: {sender} - \"{message}\"");
            Console.ResetColor();
        }

        public void MakeACall(int receiver)
        {
            MakeCallProcessing?.Invoke(this, new ConnectionEventArgs(receiver));
        }

        public void ReceiveCall(int caller)
        {
            if (IsConnectionValid(caller))
            {
                string senderName = GetSenderNameFromAddressBook(caller);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Account {Number}: Call from {caller}({senderName})");
                Console.ResetColor();

                return;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Account {Number}: Call from non-whitelisted account {caller}");
            Console.ResetColor();
        }

        protected string GetSenderNameFromAddressBook(int phoneNumber)
        {
            return Addresses.FirstOrDefault(p => p.Number == phoneNumber)?.Name;
        }

        protected bool IsConnectionValid(int phoneNumber)
        {
            return Addresses.Any(p => p.Number == phoneNumber);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.Name))
            {
                errors.Add(new ValidationResult("Name is required"));
            }

            if (string.IsNullOrWhiteSpace(this.Surname))
            {
                errors.Add(new ValidationResult("Surname is required"));
            }

            if ((this?.Name?.Length < 5 && this?.Name?.Length > 255) ||
                !Regex.IsMatch(this?.Name, "^[a-zA-Z-]*$"))
            {
                errors.Add(new ValidationResult($"Name: {this.Name}. Name has not valid format: length - from 5 to 255, symbols - alphabetic letters and dash"));
            }

            if ((this?.Surname?.Length < 5 && this?.Surname?.Length > 255) ||
                !Regex.IsMatch(this?.Surname, "^[a-zA-Z-]*$"))
            {
                errors.Add(new ValidationResult($"Surname: {this.Surname}. Surname has not valid format: length - from 5 to 255, symbols - alphabetic letters and dash"));
            }

            if (this?.BirthYear < 1930 || this?.BirthYear > 2017)
            {
                errors.Add(new ValidationResult($"Birth year: {this.BirthYear}. Birth year  has not valid format: from 1930 to 2017"));
            }

            if (!string.IsNullOrWhiteSpace(this.Email) && !Regex.IsMatch(this.Email, "^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$"))
            {
                errors.Add(new ValidationResult($"Email: {this.Email}. Email has not valid format"));
            }

            return errors;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Surname: {Surname}, Birth year: {BirthYear}, Email: {Email}, Phone: {Number}";
        }
    }
}
