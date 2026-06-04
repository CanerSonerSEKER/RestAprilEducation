using System.Diagnostics.Metrics;

namespace RestAprilEducation.API.Metrics
{
    public sealed class AppMetrics : IDisposable
    {

        public const string MeterName = "RestAprilEducation.API";

        private readonly Meter _meter; 



        /// <summary>
        /// Sürekli artan metric(Counter).
        /// Her çağrıda yalnızca arttırılabilir. Hiçbir zaman azalmaz.
        /// Örnek kullanım: Toplam sipariş sayısı, toplam istek sayısı.
        /// </summary>
        public Counter<long> OrdersCreated { get; }


        /// <summary>
        /// Hem artabilen hem azalabilen metric(UpDownCounter)
        /// Örnek Kullanım: Anlık aktif kullanıcı sayısı, kuyruktaki iş sayısı.
        /// </summary>
        public UpDownCounter<int> ActiveConnections { get; }


        public AppMetrics()
        {
            _meter = new Meter(MeterName);

            OrdersCreated = _meter.CreateCounter<long>(
                name: "orders.created",
                unit: "{order}",
                description: "Şimdiye kadar oluşturulan toplam sipariş sayısı"
                );


            ActiveConnections = _meter.CreateUpDownCounter<int>(
                name: "active.connections",
                unit: "{connection}",
                description: "Anlık aktif bağlantı sayısı"
                );
        }

        public void Dispose()
        {
            _meter.Dispose();
        }
    }
}
