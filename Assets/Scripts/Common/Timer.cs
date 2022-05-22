using System.Threading.Tasks;

namespace Common
{
    public static class Timer
    {
        public static async Task Await(int time) 
        {
            await Task.Delay(time);
        }
        
        public static async Task Await(float time) 
        {
            await Task.Delay((int)time);
        }
    }
}