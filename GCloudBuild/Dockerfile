FROM gcr.io/its-artifact-commons/php-apache:7.2-1

RUN mkdir -p /wis/system/bin
RUN mkdir -p /wis/windows
RUN chown apache2 /wis/system /wis/system/bin /wis/windows
RUN chgrp apache2 /wis/system /wis/system/bin /wis/windows

RUN echo '<h1>HELLO WORLD</h1>' > /var/www/html/index.html

COPY GCloudBuild/alias.conf /tmp
RUN cat /tmp/alias.conf >> /etc/apache2/apache2.conf
