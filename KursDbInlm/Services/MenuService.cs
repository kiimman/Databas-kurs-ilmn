using KursDbInlm.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;

namespace KursDbInlm.Services;

internal class MenuService
{
    private readonly CaseService _caseService = new CaseService();
    private readonly StatusService _statusService = new StatusService();

    public async Task MainMenu()
    {
        Console.Clear();
        Console.WriteLine("*******HUVUDMENY*******");
        Console.WriteLine();
        Console.WriteLine($"Ange något av nedanstående alternativ:");
        Console.WriteLine("1. = Visa alla rapporter");
        Console.WriteLine("2. = Skapa en rapport");
        Console.WriteLine("3. = Ändra status på specifik rapport");
        Console.WriteLine("4. = Sök efter specifik rapport");
        Console.WriteLine("");
        Console.Write("");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                await AllCasesAsync();
                break;

            case "2":
                await CreateCaseAsync();
                break;

            case "3":
                await UpdateCaseStatusAsync();
                break;

            case "4":
                await ShowSpecificCase();
                break;


            default:
                Console.Clear();
                Console.WriteLine("Ogiltigt val, pröva igen");
                break;
        }
    }

    //show all repports
    private async Task AllCasesAsync()
    {
        Console.Clear();
        Console.WriteLine("*******Alla rapporter*******" + "\n");
        foreach (var _case in await _caseService.GetAllAsync())
        {
            Console.WriteLine($"ID: {_case.Id}");
            Console.WriteLine($"Skapad: {_case.Created}");
            Console.WriteLine($"Status: {_case.Status.StatusName}");
            Console.WriteLine($"Beskrivning: {_case.Description}");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("");

        }        

    }

    //create report
    private async Task CreateCaseAsync()
    {
        var _entity = new CaseEntity();
        Console.Clear();
        Console.WriteLine("*******Registrera din rapport*******");
        Console.WriteLine("");
        Console.WriteLine("Förnamn:");     
        _entity.FirstName = Console.ReadLine() ?? "";
        Console.WriteLine("");
        Console.WriteLine("Efternamn:");
        _entity.LastName = Console.ReadLine() ?? "";
        Console.WriteLine("------------");
        Console.WriteLine("E-mail:");
        _entity.Email = Console.ReadLine() ?? "";
        Console.WriteLine("------------");
        Console.WriteLine("Telefonnummer:");
        _entity.PhoneNumber = Console.ReadLine() ?? "";
        Console.WriteLine("------------");
        Console.WriteLine("Vad vill du rapportera? ");
        _entity.Description = Console.ReadLine() ?? "";
        Console.WriteLine("---------------------------------------------------");

        await _caseService.CreateAsync(_entity);

        Console.WriteLine($"\nDin rapport är nu skapad med ID-nummer: {_entity.Id}, och har status som:{_entity.Status.StatusName} ");
        Console.WriteLine();
        Console.WriteLine("Tryck på valfri tangent för att återgå till Huvudmenyn");

    }

    


    //update specifik report status
    public async Task UpdateCaseStatusAsync()
    {
        await AllCasesAsync();

        

        Console.WriteLine("Ange Id:nr på den rapport du vill ändra status: ");
        var caseId = int.Parse(Console.ReadLine());       

        var _case = await _caseService.GetAsync(caseId);
        if (_case != null)
        {
            Console.Clear();
            Console.WriteLine($"Ändra status för rapporten med ID: {caseId} till följande:");
            Console.WriteLine("1. = Ej påbörjad");
            Console.WriteLine("2. = Påbörjad");
            Console.WriteLine("3. = Avslutad");
            Console.WriteLine("---------------");
            Console.WriteLine("");

            var option = Console.ReadLine();

            string statusName;
            int statusId;

            switch (option)
            {
                case "1":
                    statusId = 1;
                    statusName = ("Ej påbörjad");
                    break;

                case "2":
                    statusId = 2;
                    statusName = ("Påbörjad");
                    break;

                case "3":
                    statusId = 3;
                    statusName = ("Avslutad");
                    break;

                default:
                    Console.WriteLine("\nFelaktigt alternativ. Ange något av följande: 1, 2, eller 3");
                    return;
            }


            await _caseService.UpdateCaseStatusAsync(caseId, statusId);
            Console.WriteLine($"\nStatusen för rapporten med Idnummer: {caseId},  har ändrats till: {statusName}");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Tryck på valfri tangent för att återgå till Huvudmenyn");



        }
        else
        {
            Console.WriteLine("");
            Console.WriteLine($"Ingen rapport med ID:{caseId} hittades.");
            Console.WriteLine("");
            Console.WriteLine("Tryck på valfri tangent för att återgå till Huvudmenyn");

        }
        
                        
    }



    public async Task ShowSpecificCase()
    {
        await AllCasesAsync();

        Console.Write("\nAnge rapport-ID: ");
        var caseId = int.Parse(Console.ReadLine());

       
            var _case = await _caseService.GetAsync(caseId);
            if (_case != null)
            {
                Console.Clear();
                Console.WriteLine("*******Rapport-Information*******");
                Console.WriteLine();
                Console.WriteLine($"ID: {_case.Id}");
                Console.WriteLine($"Förnamn: {_case.FirstName}");
                Console.WriteLine($"Efternamn: {_case.LastName}");
                Console.WriteLine($"Email: {_case.Email}");
                Console.WriteLine($"Telefon: {_case.PhoneNumber}");
                Console.WriteLine($"Skapad: {_case.Created}");
                Console.WriteLine($"Status: {_case.Status.StatusName}");
                Console.WriteLine($"Beskrivning: {_case.Description}");
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("Tryck på valfri tangent för att återgå till Huvudmenyn");


            
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine($"Inget ärende med ärendenummer {caseId} hittades");
                Console.WriteLine("Tryck på valfri tangent för att återgå till Huvudmenyn");
            }
               
    }
}

       

