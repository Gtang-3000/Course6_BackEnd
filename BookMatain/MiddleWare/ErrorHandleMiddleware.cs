namespace BookMatain.MiddleWare
{
    public class ErrorHandleMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // 轉發請求
            }
            catch (Exception ex)
            {
                // 錯誤處理邏輯

                BookMatain.Common.Logger.Write(ex.ToString(), BookMatain.Common.Logger.LogCategoryEnum.Error);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("系統發生錯誤、請洽系統管理員");
            }
        }
    }
}
