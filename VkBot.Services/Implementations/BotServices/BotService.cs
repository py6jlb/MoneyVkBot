using VkBot.Services.Adstractions.BotServices;

namespace VkBot.Services.Implementations.BotServices
{
    public class BotService : IBotService
    {
        private readonly IVkApiService _apiService;

        public BotService(IVkApiService apiService){
            _apiService = apiService;
        }
        
        public void StartBot(){
            while (true)
            {                          
                var history = _apiService.GetHistory();                
                if (!_apiService.HistoryHasUpdtes(history))
                    continue; 
                _apiService.HandleHistoryUpdate(history);
            }
        }
    }
}