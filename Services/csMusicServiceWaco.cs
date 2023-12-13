using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using DbContext;
using Models;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace Services;

public class csMusicServiceWaco : IMusicService
{
    HttpClient _httpClient;

    #region Seeding
    public async Task<int> Seed(int NrOfitems)
    {
        string uri = $"csadmin/seed?count={NrOfitems}";
        
        //Get the response from the API
        var response = await _httpClient.GetAsync(uri);
        
        //If the response is not successful, throw an exception
        response.EnsureSuccessStatusCode();

        //Read the response as a string
        var result = await response.Content.ReadAsStringAsync();

        //Return the result as an integer
        return JsonConvert.DeserializeObject<int>(result);
        
    }
    public async Task<int> RemoveSeed()
    {
        string uri = $"csadmin/removeseed";
        //Get the response from the API
        var response = await _httpClient.GetAsync(uri);

        //If the response is not successful, throw an exception
        response.EnsureSuccessStatusCode();

        //Read the response as a string
        var result = await response.Content.ReadAsStringAsync();

        //Return the result as an integer
        return JsonConvert.DeserializeObject<int>(result);
    }
    #endregion

    #region CRUD MusicGroup
    public async Task<List<csMusicGroup>> ReadMusicGroupsAsync(bool flat)
    {
        string uri = $"csmusicgroups/read?flat={flat}";
        
        var response = await _httpClient.GetAsync(uri);
        
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<List<csMusicGroup>>(result);
        
       
    }

    public async Task<csMusicGroup> ReadMusicGroupAsync(Guid id, bool flat)
    {
        string uri = $"csmusicgroups/readitem?id={id}&flat={flat}";
        var response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<csMusicGroup>(result);
        
    }

    public async Task<csMusicGroup> UpdateMusicGroupAsync(csMusicGroupCUdto _src)
    {
        string uri = $"csmusicgroups/updateitem/{_src.MusicGroupId}";
        var json = JsonConvert.SerializeObject(_src);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(uri, data);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<csMusicGroup>(result);
    }

    public async Task<csMusicGroup> CreateMusicGroupAsync(csMusicGroupCUdto _src)
    {
        string uri = $"csmusicgroups/createitem";
        var json = JsonConvert.SerializeObject(_src);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(uri, data);
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<csMusicGroup>(result);
    
        
    }

    public async Task<csMusicGroup> DeleteMusicGroupAsync(Guid id)
    {
        string uri = $"csmusicgroups/deleteitem/{id}";
        var response = await _httpClient.DeleteAsync(uri);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<csMusicGroup>(result);
  
    }
    #endregion

    #region CRUD Albums
    public async Task<List<csAlbum>> ReadAlbumsAsync(bool flat)
    {
        string uri = $"csalbums/read?flat={flat}";
        var response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<csAlbum>>(result);
        
      
    }

    public async Task<csAlbum> ReadAlbumAsync(Guid id, bool flat)
    {
        string uri = $"csalbums/readitem?id={id}&flat={flat}";
        var response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<csAlbum>(result);
        
        
    }

    public async Task<csAlbum> CreateAlbumAsync(csAlbumCUdto _src)
    {
        
        
        string uri = $"csalbums/createitem";
                var json = JsonConvert.SerializeObject(_src);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(uri, data);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<csAlbum>(result);

       
        
    }

    public async Task<csAlbum> UpdateAlbumAsync(csAlbumCUdto _src)
    {
        string uri = $"csalbums/updateitem/{_src.AlbumId}";
        var json = JsonConvert.SerializeObject(_src);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(uri, data);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<csAlbum>(result);
        
    }

    public async Task<csAlbum> DeleteAlbumAsync(Guid id)
    {
        string uri = $"csalbums/deleteitem/{id}";
        var response = await _httpClient.DeleteAsync(uri);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<csAlbum>(result);
        
    }
    #endregion

    #region CRUD Artists
    public async Task<List<csArtist>> ReadArtistsAsync(bool flat)
    {
        string uri = $"csartists/read?flat={flat}";
        var response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<csArtist>>(result);
        
    
        
    }

    public async Task<csArtist> ReadArtistsync(Guid id, bool flat)
    {
        string uri = $"csartists/readitem?id={id}&flat={flat}";
        var response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<csArtist>(result);
        
    }

    //Upsert = Update or Insert
    public async Task<csArtist> UpsertArtistAsync(csArtistCUdto _src)
    {
        string uri = $"csartists/upsertitem";
        var json = JsonConvert.SerializeObject(_src);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(uri, data);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<csArtist>(result);
        
    }

    public async Task<csArtist> UpdateArtistAsync(csArtistCUdto _src)
    {
        string uri = $"csartists/updateitem/{_src.ArtistId}";
        var json = JsonConvert.SerializeObject(_src);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(uri, data);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<csArtist>(result);
        
    }

    public async Task<csArtist> DeleteArtistAsync(Guid id)
    {
        string uri = $"csartists/deleteitem/{id}";
        var response = await _httpClient.DeleteAsync(uri);
    response.EnsureSuccessStatusCode();
    var result = await response.Content.ReadAsStringAsync();
    return JsonConvert.DeserializeObject<csArtist>(result);
    
    }
    #endregion

    public csMusicServiceWaco(IHttpClientFactory httpClientFactory)
    {
        //Using DI for best practise usage of HttpClient.
        //Create a client from the template MusicWebApi
        _httpClient = httpClientFactory.CreateClient(name: "MusicWebApi");
    }
}
//when i run this it shows 0 albums on music group page. but when i click on the album page it shows the albums.



