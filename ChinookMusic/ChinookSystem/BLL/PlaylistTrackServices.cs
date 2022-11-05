using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using ChinookSystem.ViewModels;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTrackServices
    {
        #region Constructor for Context Dependency
        private readonly ChinookContext _context;

        internal PlaylistTrackServices(ChinookContext context)
        {
            _context = context;
        }
        #endregion

        public List<PlaylistTrackInfo> PlaylistTrack_FetchPlaylist(string playlistname, string username)
        {
            if (string.IsNullOrWhiteSpace(playlistname))
            {
                throw new ArgumentNullException("No playlist name submitted");
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException("No user name submitted");
            }
            IEnumerable<PlaylistTrackInfo> results = _context.PlaylistTracks
                                        .Where(x => x.Playlist.Name.Equals(playlistname)
                                                 && x.Playlist.UserName.Equals(username))
                                        .Select(x => new PlaylistTrackInfo
                                        {
                                            TrackId = x.TrackId,
                                            TrackNumber = x.TrackNumber,
                                            SongName = x.Track.Name,
                                            Milliseconds = x.Track.Milliseconds
                                        })
                                        .OrderBy(x => x.TrackNumber);
            return results.ToList();
        }
    }
}
