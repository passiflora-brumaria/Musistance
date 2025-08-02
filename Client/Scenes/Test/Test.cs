using Godot;
using MusistanceClient.Api;
using MusistanceClient.Api.Dto.Profile;
using MusistanceClient.Autoload;
using Newtonsoft.Json;
using System;

public partial class Test: Node
{
    private ProfileDto _profile;
    private TextureRect _pfpDisplay;
    private Label _nameDisplay;
    public override async void _Ready ()
    {
        _pfpDisplay = GetNode<TextureRect>("./Center/Column/Pfp");
        _nameDisplay = GetNode<Label>("./Center/Column/Name");

        ApiServer api = ApiServer.GetInstance(this);
        api.Configure(ApiConfiguration.Local());
        await api.InitialiseThroughItchAsync();
        _profile = await api.GetProfileAsync();

        Image img = new Image();
        img.LoadPngFromBuffer(_profile.ProfilePicture);
        ImageTexture tex = new ImageTexture();
        tex.SetImage(img);
        _pfpDisplay.Texture = tex;
        _nameDisplay.Text = _profile.Name;
    }
}
