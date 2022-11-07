
using DO;

namespace Dal;

public class DataProduct
{
    

    public int Add(Product p)
    {
        Random rn = new Random();
        p.ID = rn.Next(100000, 999999);  //check if already exist!!!!!!!
        DataSource.productsArr[DataSource.Config.productIndex] = p;
        return p.ID;
    }

    public Product Get(int id)
    {
       return DataSource.productsArr.First(x => x.ID == id);
    }

    public Product[] GetAll(int id)
    {
        Product[] products = new Product[50];
        Array.Copy(DataSource.productsArr, products, products.Length);
        return products;
    }

    public void Delete(int id)
    {
        //how?????????
    }

    public void Update(Product p)
    {
        bool found = false;
        for(int i = 0; i < DataSource.productsArr.Length; i++)
        {
            if(DataSource.productsArr[i].ID == p.ID)
            {
                found = true;
                DataSource.productsArr[i] = p;
            }
        }

        if (!found)
            throw new Exception("cannot find this product");
    }

}


//איפה נשמרת הכמות שיש במערכים
//איך מטפלים בחורים, מחיקה, הוספה וכו
//איפה מקדמים את האינדקסים
