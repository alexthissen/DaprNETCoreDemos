using System;
using System.Collections.Generic;

namespace LeaderboardWebAPI
{
    public class TweetReceived
    {
        public string Tag { get; set; }
        public Tweet Tweet { get; set; }
    }

    public class Hashtag
    {
        public IList<int> indices { get; set; }
        public string text { get; set; }
    }

    public class Url
    {
        public IList<int> indices { get; set; }
        public string display_url { get; set; }
        public string expanded_url { get; set; }
        public string url { get; set; }
    }
    public class Entities
    {
        public IList<Hashtag> hashtags { get; set; }
        public object media { get; set; }
        public IList<Url> urls { get; set; }
        public IList<UserMention> user_mentions { get; set; }
    }
    public class UserMention
    {
        public IList<int> indices { get; set; }
        public long id { get; set; }
        public string id_str { get; set; }
        public string name { get; set; }
        public string screen_name { get; set; }
    }
    public class User
    {
        public bool contributors_enabled { get; set; }
        public string created_at { get; set; }
        public bool default_profile { get; set; }
        public bool default_profile_image { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public object entities { get; set; }
        public int favourites_count { get; set; }
        public bool follow_request_sent { get; set; }
        public bool following { get; set; }
        public int followers_count { get; set; }
        public int friends_count { get; set; }
        public bool geo_enabled { get; set; }
        public long id { get; set; }
        public string id_str { get; set; }
        public bool is_translator { get; set; }
        public string lang { get; set; }
        public int listed_count { get; set; }
        public string location { get; set; }
        public string name { get; set; }
        public bool notifications { get; set; }
        public string profile_background_color { get; set; }
        public string profile_background_image_url { get; set; }
        public string profile_background_image_url_https { get; set; }
        public bool profile_background_tile { get; set; }
        public string profile_banner_url { get; set; }
        public string profile_image_url { get; set; }
        public string profile_image_url_https { get; set; }
        public string profile_link_color { get; set; }
        public string profile_sidebar_border_color { get; set; }
        public string profile_sidebar_fill_color { get; set; }
        public string profile_text_color { get; set; }
        public bool profile_use_background_image { get; set; }
        public bool Protected { get; set; }
        public string screen_name { get; set; }
        public bool show_all_inline_media { get; set; }
        public object status { get; set; }
        public int statuses_count { get; set; }
        public string time_zone { get; set; }
        public string url { get; set; }
        public int utc_offset { get; set; }
        public bool verified { get; set; }
        public object withheld_in_countries { get; set; }
        public string withheld_scope { get; set; }
    }

    public class Tweet
    {
        public object coordinates { get; set; }
        public string created_at { get; set; }
        public object current_user_retweet { get; set; }
        public Entities entities { get; set; }
        public int favorite_count { get; set; }
        public bool favorited { get; set; }
        public string filter_level { get; set; }
        public long id { get; set; }
        public string id_str { get; set; }
        public string in_reply_to_screen_name { get; set; }
        public long in_reply_to_status_id { get; set; }
        public string in_reply_to_status_id_str { get; set; }
        public long in_reply_to_user_id { get; set; }
        public string in_reply_to_user_id_str { get; set; }
        public string lang { get; set; }
        public bool possibly_sensitive { get; set; }
        public int quote_count { get; set; }
        public int reply_count { get; set; }
        public int retweet_count { get; set; }
        public bool retweeted { get; set; }
        public object retweeted_status { get; set; }
        public string source { get; set; }
        public object scopes { get; set; }
        public string text { get; set; }
        public string full_text { get; set; }
        public IList<int> display_text_range { get; set; }
        public object place { get; set; }
        public bool truncated { get; set; }
        public User user { get; set; }
        public bool withheld_copyright { get; set; }
        public object withheld_in_countries { get; set; }
        public string withheld_scope { get; set; }
    }
}
