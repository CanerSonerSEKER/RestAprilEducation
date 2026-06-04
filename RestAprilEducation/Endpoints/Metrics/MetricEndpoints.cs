using RestAprilEducation.API.Metrics;
using System.Diagnostics;

namespace RestAprilEducation.API.Endpoints.Metrics
{
    public static class MetricEndpoints
    {
        public static void AddMetricEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/metrics").WithTags("Metrics");

            //
            // Counter Endpoint -- Yalnızca artabilen metric 
            //


            // Belirtilen miktarda sipariş oluşturmuşuz gibi counter arttırıyoruz.

            group.MapPost("orders/increment", (AppMetrics metrics, int count = 1) =>
            {
                if (count <= 0)
                    return Results.BadRequest("Count 0'dan küçük olamaz");

                metrics.OrdersCreated.Add(count, new KeyValuePair<string, object?>("source", "manuel-test"));

                return Results.Ok(new
                {
                    Message = $"orders.created counter {count} arttırıldı.",
                    MetricName = "orders.created",
                    AddedValue = count
                });

            })
            .WithSummary("Counter arttır(Sadece artan)")
            .WithDescription("orders.created counterını belirtilen miktar kadar arttır."  + 
                            "Counter hiçbir zaman azalmaz - toplam oluşturulan sipariş sayısını temsil eder.");


            // ---------------------------------------------------------------
            // UpDownCounter endpoints — artabilir veya azalabilir
            // ---------------------------------------------------------------

            // Yeni bir bağlantı açıldığında çağrılır.
            group.MapPost("connections/connect", (AppMetrics metrics) =>
            {
                metrics.ActiveConnections.Add(1, new KeyValuePair<string, object?>("endpoint", "test"));

                return Results.Ok(new
                {
                    Message = "active.connections +1 artırıldı (yeni bağlantı).",
                    MetricName = "active.connections",
                    Delta = +1
                });
            })
            .WithSummary("UpDownCounter artır (bağlantı aç)")
            .WithDescription("Bir bağlantı açıldığında active.connections counter'ını 1 artırır.");


            // Bir bağlantı kapandığında çağrılır.
            group.MapPost("connections/disconnect", (AppMetrics metrics) =>
            {
                metrics.ActiveConnections.Add(-1, new KeyValuePair<string, object?>("endpoint", "test"));

                return Results.Ok(new
                {
                    Message = "active.connections -1 azaltıldı (bağlantı kapandı).",
                    MetricName = "active.connections",
                    Delta = -1
                });
            })
            .WithSummary("UpDownCounter azalt (bağlantı kapat)")
            .WithDescription("Bir bağlantı kapandığında active.connections counter'ını 1 azaltır.");


        }

    }
}
