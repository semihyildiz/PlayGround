# PlayGround
1-) DATA Katmanı.Burada Veritabanlarını yönetiyoruz.
bu katmanın appsettings.json dosyasında migration sırasında  base alınacak dbnin connection stringi var.

2-)DAL Katmanında Data Katmanına giden repositoryler var.Generic Repository pattern ve UnitOfWork yapısı bu katmanda yer alıyor.
bu katmanda Repo methodları  entititylere göre özelleşebiliyor.Bknz. RepWeatherForecast.cs

3-)Bussiness Katmanında yazılım sahibi firmanın iş mantığı bulunmaktadır.teknolojiler değişse bile burdaki iş mantığının bu classLibrary ile taşınabilmesi hedeflenilmektedir.

4-)WebApi Uygulaması=> bu uygulama ile Bussiness katmanının istenilen çıktıları dış dünya ile paylaşılır.
Dış dünyanın hava durumu istekleri. WeatherForecastController.cs adlı kontrollerda yönetilir. bu kontrolerin requestleri RequestResponseMiddleware.cs middleware'inden geçer. 

Sorgulara şu mantık ile cevap verir.
Gelen sorgu InMemory de varsa hazır veriyi döndürür.
gelen sorgu Inmemoryde yoksa, veritabanında arama yapar varsa sonucu döndürür.
gelen sorgu veritabanında yoksa 3.parti lokasyon ve hava durumu servislerini kullanarak sonucu döndürür. 3. parti sorgulaması Middle ware'e koyduğum stopwatch nesnesi ile süre olarak takip edilir. ve bu talep açık olan tüm  Monitoring uygulamasına SignalR aracılığı ile  süre bilgisi ve responsebody ile birlikte gönderilir.

In Memory Yapısı WeatherForecastController.cs 'a gelen her requestin ardından kayıtsayısı e kayıt tarihi gözetilerek Flushlanır.

5-)Monitoring adlı konsol uygulaması, bir signalr ve Http Clienttır.
ilk açıldığında MonitoringController.cs ' adresine gidip o güne ait tüm kayıtları çeker.
sonra Signalr'a baglanır. karşılıklı bir mesaj alış-verişinden sonra dinler pozisyonda kalır.
WepApiye Yeni bir query gelirse bu sonuç süre bilgisi ile birlikte ekranda görülür.




6-)Entitiy Kütüphanesi. Ortak bir kütüphanedir.Tabloları,Modelleri burada tutuyorum. Codefirst kullandığımdan, entityleri direk servisten dışarı vermekte bir sakınca görmedim. DTO nesneleri kullanmadım.

Connectionstring bilgisi WebApi projesi altında appsettings.json dosyasından değiştirilebilir.

yapılacak Sorgu için Örnek.
http://localhost:58023/weatherforecast?location=izmir 




