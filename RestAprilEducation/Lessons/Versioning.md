VERSIONING


-- Geriye uyumlama için versiyonlama yaparken, eski sürümlerin hala çalışır durumda kalmasını sağlamak önemlidir. Bu, kullanıcıların mevcut uygulamalarını kesintiye uğratmadan yeni özelliklerden yararlanabilmelerini sağlar. Eski sürümler genellikle "deprecated" olarak işaretlenir ve belirli bir süre sonra tamamen kaldırılır.

// Route Versioning -- Best Practice 
CreateAPI/v1/products

// Query String Versioning
CreateAPI/products?version=v1

// Header Versioning
header =  x-api-version: v1