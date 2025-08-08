✅ ❌ ⛏️ ⏱️ 🧱 🔐 🔑 💰 📜 📦 🧾 🧠 🔄 🚫 🟩 🔎 🏁 📈 🔥 💾 🧊 🪙 🧮 🗃️ 📡 🌐 ⚙️ 🛠️ 🔧 🗂️ 🧰 🏗️
🔓 📥 📤 🧬 🧑‍💻 🤖 🧍 🧑‍🔧 🧑‍⚖️ 🧑‍🚀 👨‍💻 👨‍🔧 👨‍⚖️ 👨‍🚀 👁️ 🧿 🔍 🔒 🏦 🏛️ 🪪 🗝️ 🧾 💳 🧷 🪙
💸 💵 💳 💼 🧾 📈 📉 🔁 🔂 🔃 🔄 🔁 🔙 🔚 🔜 🔛 🕒 🕙 🕛 ⌛ ⏳ 🧠 🧬 🦾 🧿 🛡️ 🕵️ 🔍 🪞
🌐 🛰️ 📡 🗺️ 🪐 📍 🧭 🗃️ 🗂️ 📂 🧷 🗄️ 📑 📋 🧾 🧾 🖋️ 🖊️ 📝 🔏 🔐 🔒 🔓 🗝️ 🪪
💬 🗨️ 📨 📬 📪 📫 📭 📦 📤 📥 🪫 🔋 ⚡ 🔌 🖥️ 💻 🧮 🕹️ 🛜 📱 🧱 🧊 🪵 ⚒️ ⛓️ 🔗
🚀 🧪 🛢️ 💡 🧠 🧰 🛠️ ⚙️ 🧷 📊 📉 📈 💹 🔗 ⛓️ 🪝 🏗️ 🪜 🔨 🛠️ 🧲 🔧 🔩 ⚙️ 🔋 🔌 🔍 🧪 🧬



Maroof's wallet 

{


  "publicKey": "MFYwEAYHKoZIzj0CAQYFK4EEAAoDQgAEEm2d9STVlHvD3BpsuSTplQDOpKUSQuuyLPPvNChQYyS8If5XVgPRicrVFEs8MA4ZfpE3c7fSGYebbX3catYbkw==",


  "privateKey": "MIGEAgEAMBAGByqGSM49AgEGBSuBBAAKBG0wawIBAQQgGJfMbeRQS9CbuqwcNyBPBvzZFAvnoIboPQ7V1Jzi9zehRANCAAQSbZ31JNWUe8PcGmy5JOmVAM6kpRJC67Is8+80KFBjJLwh/ldWA9GJytUUSzwwDhl+kTdzt9IZh5ttfdxq1huT"



}


Tehleem's Wallet


{
  "publicKey": "MFYwEAYHKoZIzj0CAQYFK4EEAAoDQgAEOibFBPNFNmElAoQFCEZB+ZdGj1T7BHVFjnJFRmbcO94em0DsBnWQYYJabCDPDFF0C2EQbwrsVUED1st94aKR0A==",


  "privateKey": "MIGEAgEAMBAGByqGSM49AgEGBSuBBAAKBG0wawIBAQQgaq+OP0+SPz/yoma16H3LzGq938v9D9+Vf+aWqmEKYBuhRANCAAQ6JsUE80U2YSUChAUIRkH5l0aPVPsEdUWOckVGZtw73h6bQOwGdZBhglpsIM8MUXQLYRBvCuxVQQPWy33hopHQ"
}




 const latestBlock =  chain.getLatestBlock()




      const payerBalance =  latestBlock.balanceSheet.get(fromAddress) || 0
      const payeeBalance = latestBlock.balanceSheet.get(toAddress) ||0

      if(payerBalance > amount){
            latestBlock.balanceSheet.set(toAddress , payeeBalance +amount)
            latestBlock.balanceSheet.set(fromAddress , payerBalance -amount)

             res.status(200).json({message : "transaction succesful !" , amountPaid : amount})

      }
      else{
           res.status(400).json({message : "transaction declined" , reason  : "Not enough balance"})
      }
