
using System.Threading.Tasks;

namespace Screenshot.Worker.Browser
{
    interface IBrowser
    {
        Task Screenshot(string url, string outfile);
    }
}