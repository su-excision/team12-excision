using System.Collections.Generic;
using System.Linq;
using ContosoCrafts.WebSite.Models;

/// <summary>
/// A builder class for creating instances of the ProductModel for testing
/// </summary>
namespace Namespace;
public class TestProductBuilder
{
    /// <summary>
    /// Id for the test product to be built.
    /// </summary>
    private string _id = "TST-000";

    /// <summary>
    /// Name for the test product to be built.
    /// </summary>
    private string _name = "Test Card";

    /// <summary>
    /// Value for the test product to be built.
    /// </summary>
    private float _value = 99.99f;

    /// <summary>
    /// Expansion for the test product to be built.
    /// </summary>
    private string _expansion = "Test Expansion";

    /// <summary>
    /// Rarity for the test product to be built.
    /// </summary>
    private string _rarity = "Unique";

    /// <summary>
    /// Availability for the test product to be built.
    /// </summary>
    private int _availability = 999;

    /// <summary>
    /// List of Types for the test product to be built. 
    /// </summary>
    private List<string> _type;

    /// <summary>
    /// URL to the Image for the test product to be built.
    /// </summary>
    private string _image = "https://assets.pokemon.com/assets/cms2/img/cards/web/SMA/SMA_EN_SV46.png";

    /// <summary>
    /// Description for the test product to be built
    /// </summary>
    private string _description = "A Test Card used for Testing";

    /// <summary>
    /// Array of Ratings for the test product to be built
    /// </summary>
    private int[] _ratings = new int[] { };
    /// <summary>
    /// Sets the Id for the Test Product to be built.
    /// </summary>
    /// <param name="id">The Product ID desired for testing.</param>
    /// <returns>the TestProductBuilder being Built</returns>
    public TestProductBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    /// <summary>
    /// Sets the Name for the Test Product to be built.
    /// </summary>
    /// <param name="name">The Product Name desired for testing.</param>
    /// <returns>the TestProductBuilder being built</returns>
    public TestProductBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    /// <summary>
    /// Sets the Value (price) for the Test Product to be built.
    /// </summary>
    /// <param name="value">The Product Value desired for testing.</param>
    /// <returns>the TestProductBuilder being built</returns>
    public TestProductBuilder WithValue(float value)
    {
        _value = value;
        return this;
    }

    /// <summary>
    /// Sets the Expansion for the Test Product to be built.
    /// </summary>
    /// <param name="expansion">The Expansion desired for testing.</param>
    /// <returns>the TestProductBuilder being built</returns>
    public TestProductBuilder WithExpansion(string expansion)
    {
        _expansion = expansion;
        return this;
    }

    /// <summary>
    /// Sets the Rarity for the Test Product to be built.
    /// </summary>
    /// <param name="rarity">The Rarity desired for testing.</param>
    /// <returns>the TestProductBuilder being built</returns>
    public TestProductBuilder WithRarity(string rarity)
    {
        _rarity = rarity;
        return this;
    }

    /// <summary>
    /// Adds the Availability to the Test Product to be built.
    /// </summary>
    /// <param name="availability">The Availability desired for resting.</param>
    /// <returns>the TestProductBuilder being built</returns>
    public TestProductBuilder WithAvailability(int availability)
    {
        _availability = availability;
        return this;
    }

    /// <summary>
    /// Adds the Type to the Test Product to be built.
    /// </summary>
    /// <param name="type">The Type desired for testing.</param>
    /// <returns>the TestProductBuilder being built</returns>
    public TestProductBuilder WithType(string type)
    {
        _type.Add(type);
        return this;
    }

    /// <summary>
    /// Sets the List of Types for the Test Product to be built.
    /// </summary>
    /// <param name="types">The List of Types desired for testing.</param>
    /// <returns>the TestProductBuilder being built</returns>
    public TestProductBuilder WithType(List<string> types)
    {
        _type = types;
        return this;
    }

    /// <summary>
    /// Sets the Image Link for the Test Product to be built.
    /// </summary>
    /// <param name="image">The Image Link desired for testing.</param>
    /// <returns>the TestProductBuilder being built</returns>
    public TestProductBuilder WithImage(string image)
    {
        _image = image;
        return this;
    }

    /// <summary>
    /// Sets the Description for the Test Product to be built.
    /// </summary>
    /// <param name="description">The Description desired for testing.</param>
    /// <returns>the TestProductBuilder being built</returns>
    public TestProductBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    /// <summary>
    /// Adds a rating to the Array of Ratings for the Test Product to be built.
    /// </summary>
    /// <param name="rating">The Rating desired for testing.</param>
    /// <returns>the TestProductBuilder being built</returns>
    public TestProductBuilder WithRating(int rating)
    {
        // copying the pattern from JsonFileProductService.cs
        List<int> tempList = _ratings.ToList();
        tempList.Add(rating);
        _ratings = tempList.ToArray();
        return this;
    }

    /// <summary>
    /// Sets the Ratings array for the Test Product to be built.
    /// </summary>
    /// <param name="ratings">The Array of Ratings desired for testing.</param>
    /// <returns>the TestProductBuilder being built</returns>
    public TestProductBuilder WithRatings(int[] ratings)
    {
        _ratings = ratings;
        return this;
    }

    /// <summary>
    /// Build the desired Test Product.
    /// </summary>
    /// <returns>a test ProductModel intended for testing</returns>
    public ProductModel Build()
    {
        return new ProductModel
        {
            Id = _id,
            Name = _name,
            Value = _value,
            Expansion = _expansion,
            Rarity = _rarity,
            Availability = _availability,
            Type = _type,
            Image = _image,
            Description = _description,
            Ratings = _ratings
        };
    }

}
