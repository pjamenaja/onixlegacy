FROM php:7.2-apache

RUN a2enmod rewrite && a2enmod ssl

RUN apt-get update && apt-get install -y \
        libfreetype6-dev \
        libjpeg62-turbo-dev \
        libpng-dev \
        libpq-dev \
        vim \
    && docker-php-ext-install -j$(nproc) iconv \
    && docker-php-ext-configure gd --with-freetype-dir=/usr/include/ --with-jpeg-dir=/usr/include/ \
    && docker-php-ext-install -j$(nproc) gd \
    && docker-php-ext-configure pgsql -with-pgsql=/usr/local/pgsql \
    && docker-php-ext-install pdo pdo_pgsql pgsql 

RUN useradd onix
RUN usermod -u 69690 onix
RUN groupmod -g 69690 onix

RUN mkdir -p /home/onix
RUN chown onix:onix /home/onix

ENV APACHE_RUN_USER onix
ENV APACHE_RUN_GROUP onix

RUN mkdir -p /wis/system/bin
RUN mkdir -p /wis/windows
RUN chown onix /wis/system /wis/system/bin /wis/windows
RUN chgrp onix /wis/system /wis/system/bin /wis/windows

COPY CloudBuild/alias.conf /tmp
RUN cat /tmp/alias.conf >> /etc/apache2/apache2.conf

