# WEB API Dosya Yapısı

- `Controllers/`: API isteklerini yöneten kontrolörler.
- `Middlewares/`: Özel ara katmanlar.
- `Properties/`: Proje özellik dosyaları.
- `ServiceExtensions/`: Servis genişletmeleri.
    `JwtBearerExtension.cs`: JWT (JSON Web Token) ile kimlik doğrulama eklemek için gerekli olan genişletmeleri içerir.
    `PipelineExtensions.cs`: MediatR kütüphanesi ile birlikte kullanılmak üzere bir pipeline (boru hattı) genişletmesi sağlar. AddPipelineExtensions metodu, MediatR'ın kullanacağı hizmetleri kaydeder.
    `SwaggerExtension.cs`: Swagger belgeleri oluşturmak için gerekli olan genişletmeleri içerir. AddSwaggerExtension metodu, Swagger'ı uygulamaya ekler ve API uç noktalarını keşfetmek için gerekli ayarları yapar.

- `Controllers/`: API isteklerini yöneten kontrolörler.
- `Middlewares/`: Özel ara katmanlar.
- `Properties/`: Proje özellik dosyaları.
- `ServiceExtensions/`: Servis genişletmeleri.
- `Program.cs`: Uygulamanın başlangıç noktası.
- `WebApi.csproj`: Proje dosyası.
- `appsettings.json`: Uygulama ayarları.
- `logs/`: Log dosyaları.


# Persitence Katmanı
Persistence klasörü, uygulamanın veri erişim katmanını temsil eder. Veritabanı işlemleri ve veri yönetimi ile ilgili tüm işlevleri içerir.

`Context Klasörü`
    `BaseDbContext.cs`: Temel veritabanı bağlamı sınıfı, tüm veritabanı bağlamlarının miras alacağı sınıf.
    `MsSqlDbContext.cs`: Microsoft SQL Server için özel veritabanı bağlamı.
    `MySqlDbContext.cs`: MySQL için özel veritabanı bağlamı.
    `OracleDbContext.cs`: Oracle için özel veritabanı bağlamı.
`EntityConfigurations Klasörü`
    Bu klasör, varlıkların yapılandırmalarını içerir. Örneğin, veritabanı tablolarının nasıl oluşturulacağını ve ilişkilerin nasıl ayarlanacağını belirten sınıflar burada bulunur.
`Examples/`: Örnek varlık yapılandırmaları.
`Users/`: Kullanıcı varlıkları için yapılandırmalar.
`PersistenceServiceRegistration.cs`: Persistence katmanındaki servislerin kaydedildiği dosya.
`Repositories/`: Veri erişim katmanı için repository sınıfları.
`UnitOfWorkAsync.cs`: Birim işi desenini uygulayan sınıf.

# Domain Katmanı

Domain katmanı, uygulamanın iş mantığını ve temel varlıklarını temsil eder. Bu katman, uygulamanın iş kurallarını ve veri modellerini içerir.

## Dosya Yapısı
- `GlobalUsing.cs`: Global olarak kullanılan using direktiflerini içerir.
- `Examples/`: Örnek varlıklar ve yapılandırmalar.
  - `ExampleEntity.cs`: Örnek bir varlık sınıfı.
  - `Product.cs`: Ürün varlığı için tanımlamalar.
- `Users/`: Kullanıcı ile ilgili varlıklar.
  - `User.cs`: Kullanıcı varlığı.
  - `Role.cs`: Kullanıcı rolleri için tanımlamalar.
  - `RefreshToken.cs`: Yenileme token'ları için tanımlamalar.

# Core Persistence Katmanı

Persistence katmanı, uygulamanın veri erişim katmanını temsil eder. Veritabanı işlemleri ve veri yönetimi ile ilgili tüm işlevleri içerir.

## Dosya Yapısı
- `DbHelper/`: Veritabanı yardımcı sınıfları.
    - `BaseConfiguration.cs`: ORM araçları ile kullanılmak üzere temel yapılandırmaları sağlar.
    - `DataBaseEnums.cs`: Veritabanı ile ilgili enum tanımlarını içerir.
    - `LinqExtensions.cs`: LINQ sorgularını geliştirmek için yardımcı yöntemler ve uzantı metodları içerir. 
    - `NameFunction.cs`: Veritabanında kullanılan isimlendirme işlevlerini içerebilir.
    - `SortEnums.cs`: Sıralama ile ilgili enum tanımlarını içerir.

- `Dynamic/`: Dinamik sorgular için sınıflar.
- `Paging/`: Sayfalama ile ilgili sınıflar.
- `PagingAjax/`: Ajax tabanlı sayfalama için sınıflar.
- `Repositories/`: Veri erişim katmanı için repository sınıfları.
  - EfAsyncRepositoryBase.cs: Entity Framework ile asenkron veri erişimi sağlayan temel repository sınıfı.
  - EfRepositoryBase.cs: Entity Framework ile senkron veri erişimi sağlayan temel repository sınıfı.
  - Entity.cs: Temel varlık sınıfı.
  - IAsyncRepository.cs: Asenkron repository arayüzü.
  - IEntity.cs: Varlık arayüzü.
  - IEntityTimestamps.cs: Varlık zaman damgalarını içeren arayüz.
  - IQuery.cs: Sorgu arayüzü.
  - IRepository.cs: Temel repository arayüzü.

