interface help_enType {
  [key: string]: string;
  ffMpegOptions: string;
}

const help_en: help_enType = {
  adminPassword: 'Admin Password',
  adminUserName: 'Admin Username',
  apiKey: 'API Key',
  auth: 'Authentication',
  authenticationMethod: 'Authentication Method',
  backup: 'Backup',
  cacheIcons: 'Cache Icons to the local disk to speed things up',
  cleanURLs: 'Remove URLs from being logged',
  clientUserAgent: 'Client User Agent used for downloads, epg/m3u/icons/schedules direct',
  development: 'Development',
  deviceID: 'HDHR Device ID and capability ID',
  dummyRegex: 'EPG will be set to dummy if this matches the channel EPG',
  ffMpegOptions: "FFMPeg Options: '{streamUrl}' will be replaced with the stream URL.",
  ffmPegExecutable: 'FFMPeg Executable. The name "ffmpeg(.exe)" will be searched in the OS path as well',
  filesEPG: 'Files / EPG',
  general: 'General',
  globalStreamLimit: 'Global Stream Limit for custom URLs that do not belong to a M3U playlist',
  keywordSearch: 'Keyword Search',
  m3UIgnoreEmptyEPGID: 'Ignore Streams with an empty EPG ID or EPG ID of "Dummy"',
  maxConnectRetry: 'How many times to retry receiving data from the source stream',
  maxConnectRetryTimeMS: 'Receiving Data Retry Timeout in MS',
  overWriteM3UChannels: 'Overwrite M3U Channels Numbers even if they are set',
  password: 'Password',
  rememberme: 'Remember Me',
  ringBufferSizeMB: 'Buffer Size (MB)',
  sdPassword: 'ScheduleDirect Password - The displayed value is encypted and not the real password. Re-enter the real password to change',
  sdUserName: 'ScheduleDirect Username',
  settings: 'Settings',
  signInSuccessful: 'Sign In Successful',
  signInUnSuccessful: 'Sign In Unsuccessful',
  signin: 'Sign In',
  signout: 'Sign Out',
  sslCertPassword: 'SSL Certificate Password',
  sslCertPath: 'SSL Certificate Path',
  streaming: 'Streaming',
  streamingClientUserAgent: 'Client User Agent used for video streams',
  streamingProxyType:
    'Stream Buffer Type to use. None will just use the original M3U URLs, FFMPEG - run the stream through FFMPEG, Stream Master - run the stream through SM own proxy',
  useDummyEPGForBlanks: 'Use Dummy EPG for streams with missing EPG',
  user: 'User',
  videoStreamAlwaysUseEPGLogo:
    'Always use EPG Logo for Video Stream. If the EPG is changed to one containing a logo then the video stream logo will be set to that',

  'sdSettings.alternateSEFormat': 'True: "S{0}:E{1} "  False: "s{0:D2}e{1:D2} ";'
};

export const getHelp = (key: string) => help_en[key];

export default help_en;
