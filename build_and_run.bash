#!/bin/bash

CORE_LIB_DIR=lib_wis_core_framework
ERP_LIB_DIR=lib_wis_erp_framework
SRC_SCRIPT_DIR=app_onix/onix_server/scripts

cd ${CORE_LIB_DIR}
php onix_core_framework_build.php

cd ..

cd ${ERP_LIB_DIR}
php onix_erp_framework_build.php

cd ..

cp ${CORE_LIB_DIR}/build/onix_core_framework.phar ./build
cp ${ERP_LIB_DIR}/build/onix_erp_framework.phar ./build

cp ${SRC_SCRIPT_DIR}/* ./build

export ONIX_SYM_KEY="b950ef687fef3e6f"
export ONIX_DSN="PDO:Pg:dbname=onix_prod_acd_acdesign;host=development.wintech-thai.com;port=5432"
export ONIX_DB_USERNAME="onix_prod_acd_acdesign"
export ONIX_DB_PASSWORD="4237abb3c278cd28c109d607211e0201"
export ONIX_WIP_DIR=/tmp
export ONIX_SESSION_DIR=/tmp
export ONIX_LOCK_DIR=/tmp
export ONIX_STAGE=dev

echo "Starting web server..."

php -S localhost:8000 -t build
 