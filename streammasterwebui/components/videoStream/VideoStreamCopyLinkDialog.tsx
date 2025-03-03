import { LinkButton } from '@components/buttons/LinkButton';
import { useStreamGroupsGetStreamGroupVideoStreamUrlQuery, type VideoStreamDto } from '@lib/iptvApi';
import { skipToken } from '@reduxjs/toolkit/query';
import { memo, useEffect } from 'react';

interface VideoStreamCopyLinkDialogProperties {
  readonly iconFilled?: boolean | undefined;
  readonly onClose?: () => void;
  readonly value?: VideoStreamDto | undefined;
}

const VideoStreamCopyLinkDialog = ({ iconFilled, onClose, value }: VideoStreamCopyLinkDialogProperties) => {
  const url = useStreamGroupsGetStreamGroupVideoStreamUrlQuery(value?.id ?? skipToken);

  useEffect(() => {
    if (!url.isLoading) {
      // console.log(url);
    }
  }, [url]);

  return <LinkButton link={url.data ?? ''} />;
};

VideoStreamCopyLinkDialog.displayName = 'VideoStreamCopyLinkDialog';

export default memo(VideoStreamCopyLinkDialog);
