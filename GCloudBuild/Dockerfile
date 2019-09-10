FROM gcr.io/its-artifact-commons/php-apache:7.2-1

RUN mkdir -p /wis/system/bin
RUN mkdir -p /wis/windows
RUN chown apache2 /wis/system /wis/system/bin /wis/windows
RUN chgrp apache2 /wis/system /wis/system/bin /wis/windows

RUN cd /workspace/lib_wis_core_framework \
    && php onix_core_framework_build.php \
    && cd ..

COPY lib_wis_core_framework/build/onix_core_framework.phar /wis/system/bin
COPY lib_wis_erp_framework/build/onix_erp_framework.phar /wis/system/bin
COPY app_onix/onix_server/scripts/* /wis/system/bin/
 
COPY GCloudBuild/alias.conf /tmp
RUN cat /tmp/alias.conf >> /etc/apache2/apache2.conf
