# Let's Encrypt TLS Certificates

Name | Usage | Required | SAN Urls
------ | ---- | ---- | ----
ric.bjd.demo | Ingress for the Azure Service Mesh | Yes 


## ACME Script Installation

```bash
curl https://get.acme.sh | sh
```

### Notes
> Follow [this blog](https://www.robokiwi.com/wiki/azure/dns/lets-encrypt/) to setup Let's Encrypt with Azure DNS

### Certificate Request
```bash
acme.sh --issue --dns dns_azure -d ric.bjd.demo
acme.sh --toPkcs -d ric.bjd.demo --password $PfxPASSWORD
```

## Example ENV File
```bash
➜  git:(main) ✗ cat .env
CERT_PATH=/home/brian/working/ric.bjd.demo.pfx
CERT_PFX_PASS=REPACED_WITH_PASSWORD!!!!!
```

# Navigation
[⏪ Previous Section](../docs/setup.md) ‖ [Return to Main Index 🏠](../README.md) ‖ [Next Section ⏩](../docs/infrastructure.md) 