using System.Linq.Expressions;

namespace KCode.SMITEClient.Menus;

public class MenuLogic
{
    public bool SendPing { get; private set; }
    public bool TestAuth { get; private set; }
    public bool DownloadData { get; private set; } = true;
    public bool GenerateData { get; private set; } = true;
    public bool DownloadIcons { get; private set; }
    public bool CheckRemoteImages { get; private set; }
    public bool CombineIcons { get; private set; }
    public bool GenerateHtml { get; private set; }

    public void Run()
    {
        while (true)
        {
            Print();

            var input = Console.ReadLine();
            if (input == null) throw new InvalidOperationException("It was not blocking??");
            if (input.Length == 0)
            {
                return;
            }
            if (int.TryParse(input, out var value))
            {
                switch (value)
                {
                    case 1:
                        SendPing = !SendPing;
                        break;
                    case 2:
                        TestAuth = !TestAuth;
                        break;
                    case 3:
                        DownloadData = !DownloadData;
                        if (DownloadData)
                        {
                            GenerateData = true;
                        }
                        break;
                    case 4:
                        GenerateData = !GenerateData;
                        break;
                    case 5:
                        DownloadIcons = !DownloadIcons;
                        break;
                    case 6:
                        CheckRemoteImages = !CheckRemoteImages;
                        break;
                    case 7:
                        CombineIcons = !CombineIcons;
                        break;
                    case 8:
                        GenerateHtml = !GenerateHtml;
                        break;
                }
            }
        }
    }

    private void Print()
    {
        Console.WriteLine("Current choices:");
        PrintOption(1, c => c.SendPing);
        PrintOption(2, c => c.TestAuth);
        PrintOption(3, c => c.DownloadData);
        PrintOption(4, c => c.GenerateData);
        PrintOption(5, c => c.DownloadIcons);
        PrintOption(6, c => c.CheckRemoteImages);
        PrintOption(7, c => c.CombineIcons);
        PrintOption(8, c => c.GenerateHtml);

        void PrintOption(int id, Expression<Func<MenuLogic, bool>> expr)
        {
            var memberInfo = ((MemberExpression)expr.Body).Member;
            var fn = expr.Compile();
            var value = fn.Invoke(this);
            var name = memberInfo.Name;
            Console.WriteLine($" {id}: [{(value ? "x" : " ")}] {name}");
        }
    }
}
