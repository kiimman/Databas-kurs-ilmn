using KursDbInlm.Services;

StatusService statusService = new StatusService();
MenuService menuService= new MenuService();

await statusService.CreateStatusTypesAsync();

while (true)
{
    await menuService.MainMenu();
    Console.ReadKey();
}
